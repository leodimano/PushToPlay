using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace PushToPlay.Helpers
{
    public static class FactoryHelper
    {
        public static string AdjustGameNameNumberToRoman(string gameName_)
        {            
            string[] _gameNames = gameName_.Trim().Split(' ');

            if (_gameNames.Length > 1)
            {
                switch (_gameNames[_gameNames.Length - 1])
                {
                    case "1":
                        _gameNames[_gameNames.Length - 1] = "I";
                        break;
                    case "2":
                        _gameNames[_gameNames.Length - 1] = "II";
                        break;
                    case "3":
                        _gameNames[_gameNames.Length - 1] = "III";
                        break;
                    case "4":
                        _gameNames[_gameNames.Length - 1] = "IV";
                        break;
                    case "5":
                        _gameNames[_gameNames.Length - 1] = "V";
                        break;
                    case "6":
                        _gameNames[_gameNames.Length - 1] = "VI";
                        break;
                    case "7":
                        _gameNames[_gameNames.Length - 1] = "VII";
                        break;
                    case "8":
                        _gameNames[_gameNames.Length - 1] = "VIII";
                        break;
                    case "9":
                        _gameNames[_gameNames.Length - 1] = "IX";
                        break;
                    case "10":
                        _gameNames[_gameNames.Length - 1] = "X";
                        break;
                }

                return ConvertStringArrayToString(_gameNames);
            }
            else
                return gameName_;
        }
        
        static string ConvertStringArrayToString(string[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }

        public static string AdjustGameNameRomanToNumber(string gameName_)
        {
            string[] _gameNames = gameName_.Trim().Split(' ');

            if (_gameNames.Length > 1)
            {
                switch (_gameNames[_gameNames.Length - 1])
                {
                    case "I":
                        _gameNames[_gameNames.Length - 1] = "1";
                        break;
                    case "II":
                        _gameNames[_gameNames.Length - 1] = "2";
                        break;
                    case "III":
                        _gameNames[_gameNames.Length - 1] = "3";
                        break;
                    case "IV":
                        _gameNames[_gameNames.Length - 1] = "4";
                        break;
                    case "V":
                        _gameNames[_gameNames.Length - 1] = "5";
                        break;
                    case "VI":
                        _gameNames[_gameNames.Length - 1] = "6";
                        break;
                    case "VII":
                        _gameNames[_gameNames.Length - 1] = "7";
                        break;
                    case "VIII":
                        _gameNames[_gameNames.Length - 1] = "8";
                        break;
                    case "IX":
                        _gameNames[_gameNames.Length - 1] = "9";
                        break;
                    case "X":
                        _gameNames[_gameNames.Length - 1] = "10";
                        break;
                }

                return ConvertStringArrayToString(_gameNames);
            }
            else
                return gameName_;
        }

        public static DateTime? GetSteamDate(string date_)
        {
            DateTime? _dateResult = null;
            try
            {
                _dateResult = Convert.ToDateTime(date_);
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(date_))
                {
                    _dateResult =
                        new DateTime(Convert.ToInt32(date_.Substring(7, 4)),
                            Convert.ToInt32(ConfigurationManager.AppSettings[date_.Substring(3, 3).ToUpper()]),
                            Convert.ToInt32(date_.Substring(0, 2)));
                }
            }

            return _dateResult;
        }

        public static string NumberListToString(List<int> listInt_)
        {
            StringBuilder _builder = new StringBuilder();

            if (listInt_ != null){

                foreach(int i in listInt_)
                {
                    if (_builder.Length > 0)
                        _builder.Append(",");

                    _builder.Append(i);
                }

            }

            return _builder.ToString();
        }
    }
}
