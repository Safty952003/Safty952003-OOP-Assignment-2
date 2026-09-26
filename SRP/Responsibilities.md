## 1- WardBoard

### Responsibilities
- Manage bed and patient assignments.
- Calculate patient acuity scores.
- Record pager alerts.
- Build handoff notes.
- Export ward data as CSV.

### Why this is a problem
The class has multiple independent responsibilities, so changes
to clinical rules, alerts, formatting, or exporting can require
changing the same class.

## 2- CheckoutBasket

### Responsibilities
- Manage basket items and quantities.
- Calculate subtotal and grand total.
- Parse and apply coupon codes.
- Handle gift wrapping and gift messages.
- Prepare payment authorization data.

### Why this is a problem
Basket management, discount rules, gift features, and payment
processing belong to different concerns. Changes in one area
can unnecessarily affect the same class.

### 3. SupportTicket

#### Responsibilities
- Manage ticket information and customer messages.
- Determine ticket priority from its content.
- Calculate SLA deadlines and check for breaches.
- Create public replies.
- Create internal escalation messages.

#### Why this is a problem
The class should mainly manage the support ticket itself, but it also handles priority rules, SLA calculations, and message formatting, which are separate responsibilities.

### 4. LoanDesk

#### Responsibilities
- Store and manage loan application information.
- Calculate the applicant's risk score and eligibility.
- Determine required documents.
- Generate decision letters.
- Export application data as CSV.

#### Why this is a problem
LoanDesk should focus on loan risk and eligibility, but it also handles compliance documents, letter formatting, and data export, which are separate responsibilities.

### 5. CourseEnrollmentDesk

#### Responsibilities
- Manage course registration and seat capacity.
- Manage the waitlist and promote students from it.
- Generate welcome packets.
- Generate tuition invoice lines.

#### Why this is a problem
The class should focus on enrollment and capacity, but it also handles marketing content and financial formatting, which are separate responsibilities.

### 6. KitchenTicket

#### Responsibilities
- Manage kitchen order items and ingredients.
- Detect allergens.
- Estimate preparation time.
- Render the thermal kitchen ticket.
- Determine the appropriate expo lane.

#### Why this is a problem
The class mixes kitchen operations with allergen rules and printer formatting, so changes in one area can affect unrelated responsibilities.

### 7. SubscriptionBilling

#### Responsibilities
- Manage subscription billing information.
- Calculate prorated subscription charges.
- Generate invoice numbers.
- Track failed payments.
- Generate dunning emails.
- Create accounting journal lines.

#### Why this is a problem
The class mixes billing calculations with invoice numbering, payment tracking, email content, and accounting export, which are separate reasons for change.

### 8. WarehousePickList

#### Responsibilities
- Manage warehouse picking requirements.
- Allocate available quantities.
- Determine the walking order.
- Generate picker instructions.
- Export picking data as WMS XML.

#### Why this is a problem
The class combines inventory allocation, route planning, user instructions, and system integration, which are separate responsibilities.

### 9. GradeBook

#### Responsibilities
- Record and manage student scores.
- Calculate student averages.
- Determine letter grades and honor roll status.
- Generate transcript documents.
- Export grades as CSV.

#### Why this is a problem
The class combines grade calculation and academic rules with transcript formatting and CSV export, which have different reasons to change.

### 10. AppointmentDesk

#### Responsibilities
- Manage appointment booking and availability.
- Check business hours and find available slots.
- Generate ICS calendar files.
- Create SMS reminders.

#### Why this is a problem
The class mixes appointment scheduling with calendar serialization and SMS messaging, which are separate responsibilities.