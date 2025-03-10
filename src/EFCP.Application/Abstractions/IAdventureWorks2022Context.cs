using EFCP.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCP.Application.Abstractions
{
    public interface IAdventureWorks2022Context
    {
        DbSet<Address> Addresses { get; }

        DbSet<AddressType> AddressTypes { get; }

        DbSet<AwbuildVersion> AwbuildVersions { get; }

        DbSet<BillOfMaterial> BillOfMaterials { get; }

        DbSet<BusinessEntity> BusinessEntities { get; }

        DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; }

        DbSet<BusinessEntityContact> BusinessEntityContacts { get; }

        DbSet<ContactType> ContactTypes { get; }

        DbSet<CountryRegion> CountryRegions { get; }

        DbSet<CountryRegionCurrency> CountryRegionCurrencies { get; }

        DbSet<CreditCard> CreditCards { get; }

        DbSet<Culture> Cultures { get; }

        DbSet<Currency> Currencies { get; }

        DbSet<CurrencyRate> CurrencyRates { get; }

        DbSet<Customer> Customers { get; }

        DbSet<DatabaseLog> DatabaseLogs { get; }

        DbSet<Department> Departments { get; }

        DbSet<Document> Documents { get; }

        DbSet<EmailAddress> EmailAddresses { get; }

        DbSet<Employee> Employees { get; }

        DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; }

        DbSet<EmployeePayHistory> EmployeePayHistories { get; }

        DbSet<ErrorLog> ErrorLogs { get; }

        DbSet<Illustration> Illustrations { get; }

        DbSet<JobCandidate> JobCandidates { get; }

        DbSet<Location> Locations { get; }

        DbSet<Password> Passwords { get; }

        DbSet<Person> People { get; }

        DbSet<PersonCreditCard> PersonCreditCards { get; }

        DbSet<PersonPhone> PersonPhones { get; }

        DbSet<PhoneNumberType> PhoneNumberTypes { get; }

        DbSet<Product> Products { get; }

        DbSet<ProductCategory> ProductCategories { get; }

        DbSet<ProductCostHistory> ProductCostHistories { get; }

        DbSet<ProductDescription> ProductDescriptions { get; }

        DbSet<ProductDocument> ProductDocuments { get; }

        DbSet<ProductInventory> ProductInventories { get; }

        DbSet<ProductListPriceHistory> ProductListPriceHistories { get; }

        DbSet<ProductModel> ProductModels { get; }

        DbSet<ProductModelIllustration> ProductModelIllustrations { get; }

        DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; }

        DbSet<ProductPhoto> ProductPhotos { get; }

        DbSet<ProductProductPhoto> ProductProductPhotos { get; }

        DbSet<ProductReview> ProductReviews { get; }

        DbSet<ProductSubcategory> ProductSubcategories { get; }

        DbSet<ProductVendor> ProductVendors { get; }

        DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; }

        DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; }

        DbSet<SalesOrderDetail> SalesOrderDetails { get; }

        DbSet<SalesOrderHeader> SalesOrderHeaders { get; }

        DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; }

        DbSet<SalesPerson> SalesPeople { get; }

        DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistories { get; }

        DbSet<SalesReason> SalesReasons { get; }

        DbSet<SalesTaxRate> SalesTaxRates { get; }

        DbSet<SalesTerritory> SalesTerritories { get; }

        DbSet<SalesTerritoryHistory> SalesTerritoryHistories { get; }

        DbSet<ScrapReason> ScrapReasons { get; }

        DbSet<Shift> Shifts { get; }

        DbSet<ShipMethod> ShipMethods { get; }

        DbSet<ShoppingCartItem> ShoppingCartItems { get; }

        DbSet<SpecialOffer> SpecialOffers { get; }

        DbSet<SpecialOfferProduct> SpecialOfferProducts { get; }

        DbSet<StateProvince> StateProvinces { get; }

        DbSet<Store> Stores { get; }

        DbSet<TransactionHistory> TransactionHistories { get; }

        DbSet<TransactionHistoryArchive> TransactionHistoryArchives { get; }

        DbSet<UnitMeasure> UnitMeasures { get; }

        DbSet<VAdditionalContactInfo> VAdditionalContactInfos { get; }

        DbSet<VEmployee> VEmployees { get; }

        DbSet<VEmployeeDepartment> VEmployeeDepartments { get; }

        DbSet<VEmployeeDepartmentHistory> VEmployeeDepartmentHistories { get; }

        DbSet<VIndividualCustomer> VIndividualCustomers { get; }

        DbSet<VJobCandidate> VJobCandidates { get; }

        DbSet<VJobCandidateEducation> VJobCandidateEducations { get; }

        DbSet<VJobCandidateEmployment> VJobCandidateEmployments { get; }

        DbSet<VPersonDemographic> VPersonDemographics { get; }

        DbSet<VProductAndDescription> VProductAndDescriptions { get; }

        DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; }

        DbSet<VProductModelInstruction> VProductModelInstructions { get; }

        DbSet<VSalesPerson> VSalesPeople { get; }

        DbSet<VSalesPersonSalesByFiscalYear> VSalesPersonSalesByFiscalYears { get; }

        DbSet<VStateProvinceCountryRegion> VStateProvinceCountryRegions { get; }

        DbSet<VStoreWithAddress> VStoreWithAddresses { get; }

        DbSet<VStoreWithContact> VStoreWithContacts { get; }

        DbSet<VStoreWithDemographic> VStoreWithDemographics { get; }

        DbSet<VVendorWithAddress> VVendorWithAddresses { get; }

        DbSet<VVendorWithContact> VVendorWithContacts { get; }

        DbSet<Vendor> Vendors { get; }

        DbSet<WorkOrder> WorkOrders { get; }

        DbSet<WorkOrderRouting> WorkOrderRoutings { get; }
    }
}
