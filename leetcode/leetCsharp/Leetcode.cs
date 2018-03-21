using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace leetCsharp
{
    class Leetcode
    {
        int max(int a, int b)
        {
            return a > b ? a : b;
        }
        public int FindMaxForm(string[] strs, int m, int n)  // leetcode Ones and Zeroes #474
        {
            int [,]memo = new int[m + 1, n + 1];
            for (int i=0; i<strs.Length; i++)
            {
                int ones = 0;
                int zero = 0;
                foreach (char c in strs[i])
                {
                    if (c == '1')
                        ones++;
                    else
                        zero++;
                }
                for (int r=m; r>=zero; r--)
                {
                    for (int c = n; c >= ones; c--)
                        memo[r,c] = max(memo[r,c], 1 + memo[r - zero,c - ones]);
                }
            }
            return memo[m,n];
        }

        int []dp;
        private int CombinationSum4Dp(int[] nums, int target)
        {
            if (dp[target] >= 0)
                return dp[target];
            int total = 0;
            for (int i=0; i<nums.Length; i++)
            {
                if (target >= nums[i])
                    total += CombinationSum4Dp(nums, target - nums[i]);
            }
            dp[target] = total;
            return total;
        }
        public int CombinationSum4(int[] nums, int target)  // leetcode #377
        {
            dp = new int[target + 1];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = -1;
            dp[0] = 1;
            return CombinationSum4Dp(nums, target);
        }

        // 120. Triangle
        public int MinimumTotal(IList<IList<int>> triangle, int level, int pos, IList<IList<int>> memo)
        {
            if (level == triangle.Count - 1)
                return triangle[level][pos];
            if (memo[level][pos] == Int32.MaxValue)
            {
                int left = MinimumTotal(triangle, level + 1, pos, memo);
                int right = MinimumTotal(triangle, level + 1, pos + 1, memo);
                memo[level][pos] = Math.Min(left, right) + triangle[level][pos];
            }
            return memo[level][pos];
        }
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            IList<IList<int>> memo = new List<IList<int>>(triangle.Count); // fail if use List<List<int>>
            for (int i = 0; i < triangle.Count; i++)
            {
                memo.Add(new List<int>(triangle.Count));
                for (int j = 0; j < triangle.Count; j++)
                    memo[i].Add(Int32.MaxValue);
            }
            return MinimumTotal(triangle, 0, 0, memo);
        }

        // 416. Partition Equal Subset Sum, beat 98%, 132 ms vs java 22 ms
        public bool CanPartition(int[] nums)
        {
            int sum = 0;
            foreach (int n in nums)
            {
                sum += n;
            }
            if ( (sum & 1) > 0)
                return false;
            sum /= 2;
            bool[] dp = new bool[sum + 1];
            for (int i = 1; i < dp.Length; i++)
                dp[i] = false;
            dp[0] = true;
            foreach (int n in nums)
            {
                if (sum>=n)
                {
                    dp[sum] = dp[sum] || dp[sum - n];
                    if (dp[sum])
                        return true;
                }
                for (int j = sum - 1; j > 0; j--)
                {
                    if (j >= n)
                        dp[j] = dp[j] || dp[j - n];
                }
            }
            return dp[sum];
        }
        static void Main(string[] args)
        {
        }
    }
}
