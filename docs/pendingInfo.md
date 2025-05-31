# Pending Features and Implementation Steps

## To Do

### 1. Returns Calculator
**Goal:** User can calculate loan and FD returns

**Steps:**
1. Design API Endpoint: `POST /api/Calculator/returns`
2. Implement Calculation Logic (FD: compound interest, Loan: EMI formula)
3. Create Input/Output DTOs
4. Add Service Layer for calculation
5. Add Controller action
6. Add Validation
7. Test (unit and integration)

---

### 2. EMI Repayment Simulation
**Goal:** User can check payment status and payment schedule

**Steps:**
1. Design API Endpoint: `GET /api/Loan/emi-schedule/{loanId}`
2. Fetch Loan Details from DB
3. Calculate EMI Schedule
4. Create EMI Schedule DTO
5. Add Service Layer
6. Add Controller action
7. Test (unit and integration)

---

### 3. FD Payment
**Goal:** User can check FD payment status and schedule

**Steps:**
1. Design API Endpoint: `GET /api/FD/payment-schedule/{fdId}`
2. Fetch FD Details from DB
3. Calculate Payment Schedule
4. Create Payment Schedule DTO
5. Add Service Layer
6. Add Controller action
7. Test (unit and integration)

---

## In Progress

### 4. FD Application Tracking
**Goal:** Allow customers to view status and history of their FD applications

**Steps:**
1. Design API Endpoint: `GET /api/FD/applications?userId={userId}`
2. Fetch FD Applications from DB
3. Create FD Application DTO
4. Add Service Layer
5. Add Controller action
6. Test (unit and integration)

---

### 5. Loan Application Tracking
**Goal:** Allow customers visibility into their loan application status

**Steps:**
1. Design API Endpoint: `GET /api/Loan/applications?userId={userId}`
2. Fetch Loan Applications from DB
3. Create Loan Application DTO
4. Add Service Layer
5. Add Controller action
6. Test (unit and integration)

---

## Current Auth/Authorization State
- Admin, Customer, and AdminOrCustomer policies now use the correct claim type (long URI) for role.
- JWT config, token generation, and DB role values are all correct.
- All endpoints should now authorize as expected for both Admin and Customer roles.

## Troubleshooting Checklist
- Always check the Authorization header: `Authorization: Bearer <token>`
- Decode JWT at [jwt.io](https://jwt.io/) to verify claims
- Ensure DB role values are exactly `Admin` or `Customer` (case-sensitive)
- Regenerate token after any DB or code change
- Check API console for debug output on claims and policy checks

---

**For any new feature, follow the step-by-step process above. For any auth issue, use the troubleshooting checklist.**
