<!-- Project Title -->
<h1 align="center">IrohBooks Web Application</h1>

<!-- Introduction Section -->
<p>
  I worked as the full stack developer using ASP.NET Core for this project.
</p>

<p>
  This is a comprehensive book store domain application built following the <b>MVC (Model-View-Controller)</b> architecture. The system features a robust database design and simulates a real-world e-commerce environment for browsing and ordering books.
</p>



<!-- Technical Tech Stack -->
<h3> Technical Tech Stack</h3>
<ul>
  <li><b>Framework:</b> ASP.NET Core MVC</li>
  <li><b>Database Tool:</b> Entity Framework Core (Code First)</li>
  <li><b>Database:</b> SQL Server</li>
  <li><b>Design:</b> Entity Relationship Diagram (ERD)</li>
</ul>



<!-- Key Features -->
<h3> Key Features</h3>

<h4>1. Content Management (CRUD)</h4>
<ul>
  <li>Full capabilities to Create, Read, Update, and Delete <b>Books</b>, <b>Categories</b>, and <b>Genres</b>.</li>
</ul>

<h4>2. Relationship Logic</h4>
<ul>
  <li><b>Many-to-Many:</b> Advanced linking allowing books to belong to multiple genres simultaneously.</li>
  <li><b>One-to-Many:</b> Structured tracking for user orders and account history.</li>
</ul>

<h4>3. E-commerce Essentials</h4>
<ul>
  <li><b>Shopping Cart:</b> Functional cart system for item selection and management.</li>
  <li><b>Data Seeding:</b> Automated database population with realistic data for testing and demonstration.</li>
</ul>



<!-- File Structure -->
<h3> File Structure Highlights</h3>
<ul>
  <li><code>/Models</code>: Database entities and relationship definitions.</li>
  <li><code>/Controllers</code>: Logic for handling user requests and bookstore operations.</li>
  <li><code>/Views</code>: User interface pages for browsing and shopping.</li>
  <li><code>/Data</code>: Database context and seeding configurations.</li>
</ul>



<!-- Quick Start -->
<h3> Quick Start</h3>
<ol>
  <li>Open the solution file in <b>Visual Studio</b>.</li>
  <li>Update the connection string in <code>appsettings.json</code> for your <b>SQL Server</b>.</li>
  <li>Run <code>Update-Database</code> in the Package Manager Console to create the schema.</li>
  <li>Press <b>F5</b> or <b>Fn & F5</b>  or click <b>Start</b> to launch the application.</li>
</ol>



