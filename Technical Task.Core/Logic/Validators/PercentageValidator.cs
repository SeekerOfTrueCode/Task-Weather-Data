namespace Technical_Task.Core.Logic.Validators
{
    public class PercentageValidator
    {
        private readonly IsInRangeValidator _validator;
        public PercentageValidator()
        {
            _validator = new IsInRangeValidator(0,100);
        }

        public bool IsValid(double value)
        {
            return _validator.IsValid(value);
        }
    }
}
