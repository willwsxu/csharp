using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int MinimumTotal(IList<IList<int>> triangle)
        {

        }

        static void Main(string[] args)
        {
        }
    }
}
