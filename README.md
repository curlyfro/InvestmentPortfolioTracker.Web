# Investment Portfolio Tracker - Blazor Server Web Application

A modern, responsive web application for tracking investment holdings with real-time portfolio management using Blazor Server and Bootstrap 5.

## Features

### Core Functionality
- ✅ **Dashboard** - Portfolio overview with summary statistics, top holdings, and performance metrics
- ✅ **Holdings Management** - View, add, edit, and delete investment holdings
- ✅ **Price Updates** - Individual or batch price updates with real-time calculations
- ✅ **Portfolio Analytics** - Automatic gain/loss calculations, best/worst performers
- ✅ **Filtering & Sorting** - Filter holdings by symbol/name and asset type, sortable tables
- ✅ **Responsive Design** - Mobile-friendly Bootstrap 5 interface

### Technical Features
- ✅ Real-time UI updates with Blazor Server and SignalR
- ✅ SQL Server database with stored procedures for optimal performance
- ✅ Entity Framework Core 9.0 with migrations
- ✅ Repository pattern for data access
- ✅ Service layer for business logic
- ✅ Client-side validation with data annotations
- ✅ Loading indicators and error handling
- ✅ Bootstrap 5 responsive design

## Prerequisites

- .NET 8 SDK or later
- SQL Server database (AWS RDS instance configured)
- Internet connection for database access
- Modern web browser (Chrome, Firefox, Edge, Safari)

## Database Configuration

The application connects to the same AWS RDS SQL Server instance as the console application. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=[your-rds-endpoint];Database=investment-tracker;User Id=admin;Password=***;TrustServerCertificate=True;Encrypt=True;"
  }
}
```

**Security Note**: In production, use environment variables or Azure Key Vault for sensitive connection strings.

## Installation & Setup

### 1. Navigate to Project Directory
```bash
cd InvestmentPortfolioTracker.Web
```

### 2. Restore NuGet Packages
```bash
dotnet restore
```

### 3. Update Database Connection String
Edit `appsettings.json` and update the connection string with your database credentials.

### 4. Build the Application
```bash
dotnet build
```

### 5. Run the Application
```bash
dotnet run
```

The application will:
- Automatically apply database migrations
- Start the web server (typically on https://localhost:5001)
- Open your default browser to the application

### 6. Access the Application
Navigate to: `https://localhost:5001` (or the port shown in the terminal)

## Project Structure

```
InvestmentPortfolioTracker.Web/
├── Program.cs                      # Application entry point with DI configuration
├── appsettings.json               # Configuration file
├── Models/
│   └── Holding.cs                 # Entity model with calculated properties
├── Data/
│   ├── PortfolioDbContext.cs      # EF Core database context
│   ├── PortfolioDbContextFactory.cs  # Design-time factory
│   └── Migrations/                # Database migrations
├── Repositories/
│   ├── IHoldingRepository.cs      # Repository interface
│   └── HoldingRepository.cs       # Repository with stored procedures
├── Services/
│   └── PortfolioService.cs        # Business logic and calculations
├── Pages/
│   ├── Index.razor                # Dashboard page
│   ├── Holdings.razor             # Holdings list with table
│   ├── AddHolding.razor           # Add new holding form
│   ├── EditHolding.razor          # Edit holding form
│   └── UpdatePrices.razor         # Price update interface
├── Shared/
│   ├── MainLayout.razor           # Main application layout
│   └── NavMenu.razor              # Navigation menu
└── wwwroot/
    └── css/
        └── site.css               # Custom styles
```

## Usage Guide

### Dashboard (Home)
- View portfolio summary with total value, invested amount, and gain/loss
- See top 5 holdings by current value
- Identify best and worst performing investments
- Real-time performance metrics

### Holdings Management
- **View All Holdings**: See complete list with filtering and sorting
- **Add New Holding**: Form with validation for adding investments
- **Edit Holding**: Update holding details and prices
- **Delete Holding**: Remove holdings with confirmation dialog

### Price Updates
- **Individual Updates**: Update prices one holding at a time
- **Batch Updates**: Update multiple holdings simultaneously
- Automatic last update timestamp tracking

### Features in Detail

#### Filtering & Sorting
- Filter by symbol or asset name (real-time search)
- Filter by asset type (Stock, ETF, Crypto, Bond)
- Sort by Symbol or Name (ascending/descending)

#### Calculated Fields
- Cost Basis = Quantity × Purchase Price
- Current Value = Quantity × Current Price
- Gain/Loss = Current Value - Cost Basis
- Gain/Loss % = (Gain/Loss / Cost Basis) × 100

## Technology Stack

- **.NET 8.0** - Latest framework
- **Blazor Server** - Server-side rendering with SignalR
- **C# 12** - Modern language features
- **Entity Framework Core 9.0** - ORM with stored procedures
- **SQL Server** - Database (AWS RDS)
- **Bootstrap 5** - Responsive UI framework
- **Open Iconic** - Icon library

## NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
```

## Stored Procedures

The application uses stored procedures for all database operations:

| Procedure | Purpose |
|-----------|---------|
| `sp_GetAllHoldings` | Retrieve all holdings |
| `sp_GetHoldingById` | Get specific holding |
| `sp_InsertHolding` | Create new holding |
| `sp_UpdateHolding` | Update holding details |
| `sp_UpdateHoldingPrice` | Update price only |
| `sp_DeleteHolding` | Delete holding |
| `sp_HoldingExists` | Check existence |

## Development

### Running in Development Mode
```bash
dotnet run
```

The application runs with hot reload enabled - changes to `.razor` files are automatically reflected.

### Building for Release
```bash
dotnet build -c Release
dotnet publish -c Release -o ./publish
```

### Database Migrations

#### Create New Migration
```bash
dotnet ef migrations add MigrationName
```

#### Apply Migrations
```bash
dotnet ef database update
```

#### Remove Last Migration (if not applied)
```bash
dotnet ef migrations remove
```

## Configuration

### App Settings
- **ConnectionStrings**: Database connection configuration
- **Logging**: Log level configuration
- **AllowedHosts**: CORS configuration

### Environment-Specific Settings
Create `appsettings.Development.json` or `appsettings.Production.json` for environment-specific configurations.

## Troubleshooting

### Database Connection Issues
1. Verify AWS RDS instance is running
2. Check security group allows your IP
3. Verify credentials in appsettings.json
4. Ensure SQL Server port (1433) is accessible

### Build Errors
```bash
dotnet clean
dotnet restore
dotnet build
```

### Port Already in Use
Change the port in `Properties/launchSettings.json` or use:
```bash
dotnet run --urls "https://localhost:5002"
```

### Migrations Not Applied
Migrations are automatically applied at startup. If issues occur:
```bash
dotnet ef database update --connection "your-connection-string"
```

## Security Considerations

⚠️ **Current Implementation** (Development):
- Connection string in appsettings.json
- No user authentication
- No data encryption

🔒 **Production Recommendations**:
- Use environment variables or Azure Key Vault for secrets
- Implement user authentication (ASP.NET Core Identity)
- Enable HTTPS only
- Add authorization policies
- Implement rate limiting
- Enable SQL Server auditing
- Use connection pooling
- Add input sanitization

## Performance Optimization

- **Stored Procedures**: Compiled execution plans for faster queries
- **Async/Await**: Non-blocking database operations
- **SignalR**: Real-time updates without polling
- **Scoped Services**: Proper dependency injection lifecycle
- **Response Caching**: Cache static content
- **Lazy Loading**: Load data only when needed

## Browser Support

- ✅ Chrome 90+
- ✅ Firefox 88+
- ✅ Edge 90+
- ✅ Safari 14+
- ✅ Mobile browsers (iOS Safari, Chrome Mobile)

## Comparison with Console Application

| Feature | Console App | Blazor Server App |
|---------|-------------|-------------------|
| User Interface | Text-based menu | Modern web UI |
| Accessibility | Local only | Network accessible |
| Real-time Updates | Manual refresh | Automatic SignalR |
| Multi-user Support | Single user | Multi-user capable |
| Responsiveness | Terminal window | Mobile responsive |
| Data Visualization | Text tables | Rich UI components |

## Future Enhancements

- 📊 Charts and graphs (Chart.js integration)
- 📈 Historical price tracking
- 💹 Transaction history
- 🔐 User authentication
- 📱 PWA support
- 📧 Email notifications
- 📤 Export to Excel/CSV
- 🌐 API integration for live prices
- 🔄 Multiple portfolio support

## License

This is a portfolio project for educational purposes.

## Contributing

This is a personal project, but suggestions and feedback are welcome!

## Support

For issues or questions, please refer to the main project documentation or create an issue in the repository.

---

**Built with ❤️ using Blazor Server and .NET 8**