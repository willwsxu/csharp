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

        public bool canIWin(int maxChoosableInteger, int mask, int total, int desiredTotal, int fullmask, int[]dp)
        {
            if (total >= desiredTotal || mask == fullmask) // not allowed to pick any number
                return false;
            if (dp[mask] >= 0)
                return dp[mask] > 0;
            bool ans = false;
            for (int i = 1; i <= maxChoosableInteger; i++)
            {
                int m = 1 << (i - 1);
                if ((m & mask) > 0) // number is used
                    continue;
                if (!canIWin(maxChoosableInteger, mask | m, total + i, desiredTotal, fullmask, dp))
                {
                    ans = true; // you win when other player lose
                    break;
                }
            }
            dp[mask] = ans ? 1 : 0;
            return ans;
        }
        public bool CanIWin(int maxChoosableInteger, int desiredTotal)
        {
            int sumAll = (1 + maxChoosableInteger) * maxChoosableInteger / 2;
            if (sumAll < desiredTotal)
                return false;
            if (sumAll == desiredTotal)
                return maxChoosableInteger % 2 > 0; // odd wins
            if (desiredTotal <= maxChoosableInteger)  // win with 1 move
                return true;
            int fullmask = (1 << maxChoosableInteger) - 1;
            int[]dp = new int[fullmask + 1];
            for (int i = 0; i < dp.Length; i++)
                dp[i] = -1;
            return canIWin(maxChoosableInteger, 0, 0, desiredTotal, fullmask, dp);
        }

        // There is an m by n grid with a ball.Given the start coordinate (i, j) of the ball, you can move the ball to adjacent cell or 
        // cross the grid boundary in four directions(up, down, left, right). However, you can at most move N times.Find out the number 
        // of paths to move the ball out of grid boundary.The answer may be very large, return it after mod 109 + 7.
        // Once you move the ball out of boundary, you cannot move it back.
        // The length and height of the grid is in range[1, 50].
        // N is in range[0, 50].
        public int FindPaths(int m, int n, int N, int i, int j)
        {
            int[,,] dp3 = new int[51, 50, 50];
            const int MOD = 1000000007;
            if (N < 0)
                return 0;
            for (int x=1; x<=N; x++)
            {
                for (int r=0; r<m; r++)
                {
                    for (int c=0; c<n; c++)
                    {
                        dp3[x, r, c] += r == 0 ? 1 : dp3[x - 1, r - 1, c];  // up !ERROR don't use i-- etc
                        dp3[x, r, c] %= MOD;
                        dp3[x, r, c] += r == m-1 ? 1 : dp3[x - 1, r + 1, c];  // down
                        dp3[x, r, c] %= MOD;
                        dp3[x, r, c] += c == 0 ? 1 : dp3[x - 1, r, c-1];  // left
                        dp3[x, r, c] %= MOD;
                        dp3[x, r, c] += c == n-1 ? 1 : dp3[x - 1, r, c + 1];  // left
                        dp3[x, r, c] %= MOD;
                    }
                }

            }
            return dp3[N,i,j];
        }
        static void Main(string[] args)
        {
            Leetcode test = new Leetcode();
            Console.Out.WriteLine(test.FindPaths(2, 2, 2, 0, 0));
            Console.Out.WriteLine(test.FindPaths(1, 3, 3, 0, 1));
        }
    }
}
