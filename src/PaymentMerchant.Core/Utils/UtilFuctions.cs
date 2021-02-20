using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentMerchant.Core.Utils
{
    public class UtilFuctions
    {
        public static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both checksums MOD 10 equals 0
            return ((checkSum % 10) == 0);
        }



        public static int GetDaysDiff(DateTime firstDate, DateTime secondDate)
        {
               return (firstDate - secondDate).Days;
        }

        public static bool  IsEqualOrGreater(DateTime firstDate, DateTime secondDate)
        {
            return  firstDate.Date >= secondDate.Date ;
        }

    }
}
