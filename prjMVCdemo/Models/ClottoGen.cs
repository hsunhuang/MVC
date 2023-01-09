using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMVCdemo.Models
{
    public class ClottoGen
    {
        public string getNumbers()
        {
            //數字1-49取6個亂數
            Random random = new Random();
            int count = 0;
            int[] numbers = new int[6];
            while (count < 6)
            {
                int temp=random.Next(1,50);
                bool isSelected=false;
                for (int i=0;i<numbers.Length;i++)
                {
                    if (temp == numbers[i])
                    {
                        isSelected=true;
                        break;
                    }
                }
                if (!isSelected)
                {
                    numbers[count] = temp;
                    count++;
                }
            }

            //排序
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length-1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        int big = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = big;
                    }
                }
            }

            string s = "";
            foreach(int i in numbers)
            s+=i.ToString()+"";
            return s;

        }
    }
}