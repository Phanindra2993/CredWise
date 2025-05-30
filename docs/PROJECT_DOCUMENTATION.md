# CredWise Application Documentation

## Project Overview
CredWise is an Angular-based web application that provides a platform for managing credentials and user authentication. The application is built using Angular 19.2.0 and incorporates modern web development practices.

## Technology Stack
- **Frontend Framework**: Angular 19.2.0
- **UI Framework**: NG-ZORRO (Ant Design for Angular)
- **State Management**: RxJS
- **Styling**: SCSS
- **TypeScript**: 5.7.2

## Project Structure
```
src/
├── _models/         # Data models and interfaces
├── _services/       # Service layer for API calls and business logic
├── _guards/         # Route guards for authentication
├── app/            # Main application code
│   ├── pages/      # Feature modules
│   │   ├── dashboard/
│   │   ├── login/
│   │   └── register/
│   ├── shared/     # Shared components and utilities
│   └── app.component.* # Root component files
├── styles.scss     # Global styles
└── main.ts         # Application entry point
```

## Key Features
1. **Authentication**
   - User registration
   - User login
   - Route protection using guards

2. **Dashboard**
   - Main application interface
   - User-specific content and features

3. **Shared Components**
   - Reusable UI components
   - Common utilities and services

## Development Setup
1. **Prerequisites**
   - Node.js
   - Angular CLI (version 19.2.6 or higher)

2. **Installation**
   ```bash
   npm install
   ```

3. **Running the Application**
   ```bash
   npm start
   ```
   The application will be available at `http://localhost:4200`

4. **Building for Production**
   ```bash
   npm run build
   ```

## Project Configuration
- **Angular Configuration**: `angular.json`
- **TypeScript Configuration**: `tsconfig.json`
- **Package Management**: `package.json`

## Dependencies
### Main Dependencies
- @angular/common: ^19.2.0
- @angular/core: ^19.2.0
- @angular/forms: ^19.2.0
- @angular/router: ^19.2.0
- ng-zorro-antd: ^19.2.2
- rxjs: ~7.8.0

### Development Dependencies
- @angular-devkit/build-angular: ^19.2.6
- @angular/cli: ^19.2.6
- typescript: ~5.7.2
- karma and jasmine for testing

## Best Practices
1. **Code Organization**
   - Follow Angular's modular architecture
   - Separate concerns using services and components
   - Use lazy loading for feature modules

2. **Styling**
   - Use SCSS for component-specific styles
   - Follow BEM methodology for CSS class naming
   - Maintain consistent theming using NG-ZORRO

3. **TypeScript**
   - Use strong typing
   - Implement interfaces for data models
   - Follow Angular's style guide

## Testing
The project uses Jasmine and Karma for testing:
- Unit tests: `*.spec.ts` files
- Run tests: `npm test`

## Deployment
1. Build the application using `npm run build`
2. The production build will be available in the `dist` directory
3. Deploy the contents of the `dist` directory to your web server

## Contributing
1. Follow the existing code structure and patterns
2. Write tests for new features
3. Update documentation when adding new features
4. Use meaningful commit messages

## Support
For any issues or questions, please refer to the project's issue tracker or contact the development team. 