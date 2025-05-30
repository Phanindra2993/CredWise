# CredWise - Loan Management Application

## Project Overview
CredWise is a modern Angular-based loan management application that helps users explore and manage different types of loans including Home, Personal, and Gold loans. The application provides a user-friendly interface for viewing loan details, comparing options, and understanding the loan process.

## Tech Stack
- **Frontend Framework**: Angular 17
- **Styling**: SCSS
- **Mock Backend**: JSON Server
- **Development Server**: Vite
- **Package Manager**: npm

## Project Structure
```
src/
├── app/
│   ├── dashboard/         # Main dashboard components
│   ├── pages/            # Various page components
│   ├── services/         # API and business logic services
│   ├── shared/           # Shared components and utilities
│   └── _models/          # TypeScript interfaces and models
├── assets/               # Static assets
└── styles/              # Global styles
```

## Key Features
1. **Loan Product Display**
   - Home Loans
   - Personal Loans
   - Gold Loans
   - Detailed loan information including interest rates, tenure, and requirements

2. **How It Works Section**
   - Step-by-step loan process explanation
   - Visual guides for loan application

3. **Responsive Design**
   - Mobile-first approach
   - Modern UI components
   - Responsive grid layout

## Data Models

### Loan Model
```typescript
interface Loan {
  id: number;
  image: string;
  title: string;
  description: string;
  maxLoanAmount: number;
  loanType: 'HOME' | 'PERSONAL' | 'GOLD';
  interestRate: number;
  tenureMonths: number;
  processingFee: number;
  downPaymentPercentage?: number;
  minSalaryRequired?: number;
  goldPurityRequired?: string;
  repaymentType: string;
  documentsRequired: string[];
}
```

## API Endpoints
- Base URL: `http://localhost:3000`
- Loan Products: `/data`
- How It Works: `/howitworks`

## Development Setup
1. Install dependencies:
   ```bash
   npm install
   ```

2. Start JSON Server (mock backend):
   ```bash
   npx json-server --watch db.json --port 3000
   ```

3. Start Angular development server:
   ```bash
   ng serve
   ```

## Known Issues and Solutions
1. **JSON Server Connection**
   - Ensure JSON server is running on port 3000
   - Use correct endpoint (/data) for loan products
   - Check for CORS issues if API calls fail

2. **Data Loading**
   - Products may show as undefined if API response structure doesn't match model
   - Ensure proper error handling in components

## Future Enhancements
1. User authentication
2. Loan application form
3. Document upload functionality
4. Loan calculator
5. Admin dashboard

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
This project is licensed under the MIT License. 