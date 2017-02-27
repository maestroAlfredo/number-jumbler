using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NumberJumbler.Models
{
    public static class INumberJumblerExtensions
    {
        private static bool charMatchesAnyDelimiter(char [] delimiters, char c)
        {
            bool result = false;
            foreach (var del in delimiters)
            {
                result |= del == c;
                if (result) break;
            }
            return result;
        }

        /// <summary>
        /// reads an input stream and returns strings seperated by the specified delimiters
        /// </summary>
        /// <param name="reader">the reader</param>
        /// <param name="delimiters">the delimiters</param>
        /// <returns></returns>
        public static IEnumerable<string> ReadString(this StreamReader reader, char[] delimiters)
        {
            List<char> chars = new List<char>();
            while (reader.Peek() >= 0)
            {
                char c = (char)reader.Read();

                if(charMatchesAnyDelimiter(delimiters,c))
                {
                    yield return new string(chars.ToArray());
                    chars = new List<char>();
                    continue;
                }

                chars.Add(c);
                
            }
        }
    }

    public class NumberJumblerServices : INumberJumblerService
    {
        /// <summary>
        /// finds the leader
        /// </summary>
        /// <param name="input"></param>
        /// <returns>-1 if the leader is not found</returns>
        public NumberJumblerResult FindLeader(Stream input)
        {
            string errorMessage = "";
            Stream output = new MemoryStream();
            input.CopyTo(output);
            input.Position = 0;
            if(isValid(input,out errorMessage))
            {
                output.Position = 0;
                return FindLeaderNoValidation(output);
            }
            else
            {
                return new NumberJumblerResult()
                {
                    ErrorMessage = errorMessage,
                    HasErrors = false,
                    Result = -1
                };
            }
        }

        /// <summary>
        /// checks if the input is valid
        /// </summary>
        /// <param name="input">string which is either comma delimited or semicolon delimited</param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool isValid(Stream input, out string errorMessage)
        {
            errorMessage = "";
            bool result = false;
            int prevNumber = -1;
            IEnumerable<string> numbers;
            int length = 0;

            using (var reader = new StreamReader(input))
            {

                numbers = reader.ReadString(new char[] { ',', ';', '\n' });
                foreach(var number in numbers)
                {
                    length++;
                    if (length > 100000)
                    {
                        result = false;
                        errorMessage = "Number is too long, Please check the length of the csv";
                        break;
                    }
                    int convertedNumber = -1;
                    result = int.TryParse(number, out convertedNumber);
                    if (!result)
                    {
                        convertedNumber = -1;
                        errorMessage = "Integer in file cannot be converted. Check that the value is within the range specified";
                        break;
                    }
                    else
                    {
                        //handle less than 0 cases that lie within the int.MaxValue and int.MinValue
                        if (convertedNumber < 0)
                        {
                            result = false;
                            errorMessage = "Negative number detected in input file.";
                            break;
                        }

                        if (convertedNumber < prevNumber)
                        {
                            result = false;
                            errorMessage = "Values not in increasing order. Please check order and try again";
                            break;
                        }
                    }
                    prevNumber = convertedNumber;
                }
                if (length == 0) errorMessage = "Empty file detected";

            }
            return result;
            
        }

        public NumberJumblerResult FindLeaderNoValidation(Stream input)
        {
            int value = -1;
            int currentCount = 0;
            int maxCount = 0;
            int maxOccuringValue = -1;
            int currentValue = -1;
            int previousValue = -1;
            int length = 0;

            using(var reader = new StreamReader(input))
            {
                var numbers = reader.ReadString(new char[] { ',', ';', '\n' });
                foreach (var num in numbers)
                {
                    length++;
                    if (int.TryParse(num, out value))
                    {
                        currentValue = value;
                        if (currentValue == previousValue) //if the numbers are the same update the current count
                        {
                            currentCount++; //update the current count
                        }
                        else
                        {
                            if (currentCount > maxCount)
                            {
                                maxCount = currentCount;
                                maxOccuringValue = previousValue;
                            }
                            currentCount = 1; //resets the current counter.

                        }

                    }
                    previousValue = currentValue;
                };
            }

            return new NumberJumblerResult() { ErrorMessage = "", HasErrors = false, Result = (maxCount>length/2)? maxOccuringValue : -1};

        }
    }
}