using System;
namespace VeeOne.ESP.Utilities
{
    public class Utilities
    {
        public static void doCallback(object _owner, string _function, Type _dataType, object _data)
        {
        }

        public static int getPixelFromDp(int _dp, float _scale)
        {   // '0.5f' is added to round the px value to the neares whole number.
            return (int)(_dp * _scale + 0.5f);
        }
    }
}
