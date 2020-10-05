using System;

namespace Restaurant.BusinessEntities
{
    public class FoodProducts
    {
        #region Fields

        private long foodID = 0;
        private long foodProductId = 0;
        private long foodCategoryId = 0;
        private long price = 0;
        private string foodName = String.Empty;
        private long categoryID = 0;
        private string categoryName = string.Empty;
        private long companyID = 0;
        private string description = String.Empty;
        private long createdBy = 0;
        private DateTime createdAt = DateTime.MaxValue;
        private long modifiedBy = 0;
        private DateTime modifiedAt = DateTime.MaxValue;
        private decimal taxPercent = Decimal.Zero;
        private string isDiscountable = String.Empty;
        private byte[] imageSource;
        private string type;

        #endregion

        public byte[] ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }
        #region Properties
        /// <summary>
        /// Gets or sets the ProductID value.
        /// </summary>
        public long FoodID
        {
            get { return foodID; }
            set { foodID = value; }
        }
        
        public long FoodProductId
        {
            get { return foodProductId; }
            set { foodProductId = value; }
        }
        public long FoodCategoryId
        {
            get { return foodCategoryId; }
            set { foodCategoryId = value; }
        }
        public long Price
        {
            get { return price; }
            set { price = value; }
        }
        /// <summary>
        /// Gets or sets the ProductName value.
        /// </summary>
        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }

        /// <summary>
        /// Gets or sets the CategoryID value.
        /// </summary>
        public long CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        /// <summary>
        /// Gets or sets the OrgID value.
        /// </summary>
        public long CompanyID
        {
            get { return companyID; }
            set { companyID = value; }
        }



        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the CreatedBy value.
        /// </summary>
        public long CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        /// <summary>
        /// Gets or sets the CreatedAt value.
        /// </summary>
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        /// <summary>
        /// Gets or sets the ModifiedBy value.
        /// </summary>
        public long ModifiedBy
        {
            get { return modifiedBy; }
            set { modifiedBy = value; }
        }

        /// <summary>
        /// Gets or sets the ModifiedAt value.
        /// </summary>
        public DateTime ModifiedAt
        {
            get { return modifiedAt; }
            set { modifiedAt = value; }
        }



        /// <summary>
        /// Gets or sets the TaxPercent value.
        /// </summary>
        public decimal TaxPercent
        {
            get { return taxPercent; }
            set { taxPercent = value; }
        }

        /// <summary>
        /// Gets or sets the IsDiscountable value.
        /// </summary>
        public string IsDiscountable
        {
            get { return isDiscountable; }
            set { isDiscountable = value; }
        }
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        #endregion

    }
}
