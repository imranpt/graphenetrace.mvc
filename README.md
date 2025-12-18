Graphene Trace – Sensore Platform
📋 Project Overview
Graphene Trace is a MedTech startup based in Chelmsford, developing Sensore – a continuous and automated pressure ulcer prevention system. Sensore uses e-textile pressure mapping and AI-driven "smart alerts" to monitor and analyze pressure distribution in real-time.

This project involves building a data management, analysis, and visualization platform for Sensore's pressure mapping data, supporting three user roles: patients, clinicians, and administrators.

📊 Data Format
Pressure mapping data is generated as real-time heat maps, formatted as a time-ordered array of 32x32 matrices.

Each matrix is saved as a CSV file, organized by user ID and timestamp.

Pressure values range from 1 to 255:

1 = zero force (default)

255 = maximum pressure (saturation)

Values scale linearly with applied pressure.

Example data structure (per frame):

text
32 columns × 32 rows = 1024 sensor pixels
Refer to Figure 1 in the document for a sample CSV layout.

🎯 User Requirements
1. Database Structure
Store time-ordered pressure map data per user.

Support efficient querying by user, time range, and alert status.

2. User Roles & Access
Patient: View own data, receive alerts, submit feedback.

Clinician: Access data for one or multiple patients, review alerts and comments.

Admin: Create and manage user accounts.

3. Pressure Analysis & Alerts
Detect high-pressure regions in real-time.

Flag risky periods in the database for clinician review.

4. Key Metrics Extraction
Peak Pressure Index: Highest pressure value in a frame (excluding areas < 10 pixels).

Contact Area %: Percentage of pixels above a lower threshold (indicating body contact).

5. Time-Based Visualizations
Graphs showing metric trends over selectable periods:

Last hour

Last 6 hours

Last 24 hours

Custom ranges

6. Reporting
Generate user-friendly reports comparing:

Day-to-day changes

This hour vs. yesterday

Other historical comparisons

7. User Feedback System
Patients can submit comments tied to specific pressure map timestamps.

Clinicians can review and reply to comments in-thread.

🧠 Nice-to-Have Features
Extract additional metrics beyond Peak Pressure Index and Contact Area %.

Enhance visualizations for better aesthetics and UX.

Ensure data representation is intuitive for non-technical users.

🗂️ File Structure (Proposed)
text
graphene-trace/
├── data/
│   ├── raw/               # Raw CSV files per user/timestamp
│   ├── processed/         # Cleaned and labeled data
│   └── database/          # SQLite/PostgreSQL schema & scripts
├── src/
│   ├── analysis/          # Pressure analysis & alert logic
│   ├── visualization/     # Heat maps, graphs, dashboards
│   ├── auth/              # User authentication & role management
│   ├── reporting/         # Report generation modules
│   └── api/               # REST API for frontend-backend communication
├── frontend/
│   ├── patient-dash/      # Patient dashboard
│   ├── clinician-dash/    # Clinician dashboard
│   └── admin-panel/       # Admin interface
├── docs/
│   ├── figures/           # Screenshots & diagrams
│   └── requirements.md    # Detailed specs
└── README.md
🛠️ Tech Stack Suggestions
Backend: Python (Flask/Django), Node.js, or .NET Core

Database: PostgreSQL (time-series support) or MongoDB

Data Processing: Pandas, NumPy

Visualization: Plotly, D3.js, Matplotlib

Frontend: React.js or Vue.js

Authentication: JWT, OAuth 2.0

Deployment: Docker, AWS/Azure, CI/CD via GitHub Actions

🚀 Getting Started
Clone the repository

bash
git clone https://github.com/your-repo/graphene-trace.git
cd graphene-trace
Set up environment

bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
pip install -r requirements.txt
Configure database

bash
cd src/database
python init_db.py
Run the application

bash
flask run  # or npm start for frontend
📌 Next Steps
Finalize database schema

Implement user authentication

Develop pressure analysis module

Build clinician dashboard

Integrate real-time alerting

User testing & feedback iteration

📄 License
This project is proprietary and developed for Graphene Trace Ltd.
All rights reserved.

📧 Contact
For more information, contact the Graphene Trace team at Chelmsford, UK.



