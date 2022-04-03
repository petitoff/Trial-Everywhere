using System;
using System.Collections.Generic;
using System.Linq;

namespace Trial_Everywhere
{
    internal class CreditCardNumberGenerator
    {
        public List<string> CardNumberFoundList = new List<string>();

        public CreditCardNumberGenerator(string CardNumberPrefix)
        {
            this.cardNumberPrefix = CardNumberPrefix;
        }
        private string cardNumberPrefix;

        public void GetCreditCardNumbers(int howMany)
        {
            int checkLength = cardNumberPrefix.Length;
            int howMushIsMissing = 16 - checkLength;

            if (howMushIsMissing > 6) return;

            Random rnd = new Random();

            while (CardNumberFoundList.Count < howMany)
            {
                string prefix = "";
                for (int i = 0; i < 6; i++)
                {
                    prefix += rnd.Next(0, 9).ToString();
                }

                if (IsValidCreditCardNumber(cardNumberPrefix + prefix))
                {
                    CardNumberFoundList.Add(cardNumberPrefix + prefix);
                }
            }
        }

        private bool IsValidCreditCardNumber(string cardNumber)
        {
            //string creditCard = "5297501100132586";
            return SumAllNumber(cardNumber) == 0;
        }

        private int SumAllNumber(string cardNumber)
        {
            List<int> cardNumberList = cardNumber.Select(i => Convert.ToInt32(i.ToString())).ToList();

            int sumAllNumbers = 0;
            for (int i = 0; i < cardNumberList.Count(); i++)
            {

                if (i % 2 == 0)
                {
                    string result = (cardNumberList[i] * 2).ToString();
                    if (result.Length == 2)
                    {
                        sumAllNumbers += Convert.ToInt32(result[0].ToString()) + Convert.ToInt32(result[1].ToString());
                    }
                    else
                    {
                        sumAllNumbers += Convert.ToInt32(result);

                    }
                }
                else
                {
                    sumAllNumbers += cardNumberList[i];
                }
            }

            return sumAllNumbers % 10;
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
