namespace IEvangelist.CSharp.Seven.Features
{
    class BinaryLiterals
    {
        // These have been around
        public const int Hex = 0x10;    // Hexadecimal
        public const int Dec = 7;       // Decimal
        public const int Oct = 076;     // Octal
        public const double Sci = 10e1; // Scientific

        // C# 7 now supports binary literals 
        // The 0b at the beginning of the constant indicates that the 
        // number is written as a binary number.Binary numbers can
        // get very long, so it's often easier to see the bit patterns
        // by introducing the _ as a digit separator

        public const int Sixteen = 0b0001_0000;

        public const int ThirtyTwo = 0b0010_0000;

        public const int SixtyFour = 0b0100_0000;

        public const int OneHundredTwentyEight = 0b1000_0000;

        public const long BillionsAndBillions = 100_000_000_000;

        public const double AvogadroConstant =
            6.022_140_857_747_474e23;

        public const decimal GoldenRatio =
            1.618_033_988_749_894_848_204_586_834_365_638_117_720_309_179M;
    }
}
