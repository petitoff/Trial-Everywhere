using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trial_Everywhere
{
    internal class CreditCardNumberGenerator
    {
        public CreditCardNumberGenerator()
        {

        }

        public string GetCreditCardNumbers(string prefix)
        {
            return null;
        }

        private bool IsValidCreditCardNumber(string creditCardNumber)
        {
            //string creditCard = "5297501100132586";
            List<int> creditCardList = creditCardNumber.Select(i => Convert.ToInt32(i.ToString())).ToList();

            int sumAllNumbers = 0;
            for (int i = 0; i < creditCardList.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    sumAllNumbers += creditCardList[i] * 2;
                }
                else
                {
                    sumAllNumbers += creditCardNumber[i];
                }
            }

            return sumAllNumbers % 10 == 0;
        }

        private string ReverseString(string text)
        {
            if (text == null) return null;

            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new String(array);
        }
    }
}
