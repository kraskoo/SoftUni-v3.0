namespace Models.ValidationService
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using BindingModels;

    public class GameValidationService : ValidationServiceGeneric<ManageGameBindingModel>
    {
        private IDictionary<string, string> invalidProperties;

        public GameValidationService(ManageGameBindingModel bindingModel) : base(bindingModel)
        {
            this.invalidProperties = new Dictionary<string, string>();
            this.ExecuteValidations();
        }

        public override IDictionary<string, string> InvalidProperties => new Dictionary<string, string>(this.invalidProperties);

        protected override void AppendNewInvalidValidation(string reasonField, string invalidMessage)
        {
            if (!string.IsNullOrEmpty(invalidMessage))
            {
                this.invalidProperties.Add(reasonField, invalidMessage);
            }
        }

        protected sealed override void ExecuteValidations()
        {
            this.ValidateTitle();
            this.ValidatePrice();
            this.ValidateSize();
            this.ValidateTrailer();
            this.ValidateThumbnailUrl();
            this.ValidateDescription();
        }

        private void ValidateTitle()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                Regex.IsMatch(this.BindingModel.Title, @"([A-Z][\w\W]){2,99}"), Constants.InvalidGameTitleMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Title), validation.ToString());
        }

        private void ValidatePrice()
        {
            Validation validation = new Validation();
            validation.CheckUp(this.BindingModel.Price >= 1, Constants.InvalidGamePriceMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Price), validation.ToString());
        }

        private void ValidateSize()
        {
            Validation validation = new Validation();
            validation.CheckUp(this.BindingModel.Size >= 1, Constants.InvalidGameSizeMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Size), validation.ToString());
        }

        private void ValidateTrailer()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                Regex.IsMatch(this.BindingModel.Trailer, @"([\w\W]){11}"),
                Constants.InvalidGameYoutubeId);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Trailer), validation.ToString());
        }

        private void ValidateThumbnailUrl()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                Regex.IsMatch(
                    this.BindingModel.ImageThumbnail,
                    @"^(https?:\/\/[\w\W])+"),
                Constants.InvalidGameImageThumbnailMessage);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.ImageThumbnail), validation.ToString());
        }

        private void ValidateDescription()
        {
            Validation validation = new Validation();
            validation.CheckUp(
                Regex.IsMatch(this.BindingModel.Description, @"([\w\W]){20,}"), Constants.InvalidGameDescription);
            this.AppendNewInvalidValidation(nameof(this.BindingModel.Description), validation.ToString());
        }
    }
}