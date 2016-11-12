namespace CodeFirstFromDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public partial class Employee
    {
        private ICollection<Department> departments;
        private ICollection<Employee> managerEmployees;
        private ICollection<Project> projects;

        [SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.departments = new HashSet<Department>();
            this.managerEmployees = new HashSet<Employee>();
            this.projects = new HashSet<Project>();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string JobTitle { get; set; }

        public int DepartmentID { get; set; }

        public int? ManagerID { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public int? AddressID { get; set; }

        public virtual Address Address { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments
        {
            get { return this.departments; }
            set { this.departments = value; }
        }

        public virtual Department Department { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> ManagerEmployees
        {
            get { return this.managerEmployees; }
            set { this.managerEmployees = value; }
        }

        public virtual Employee Manager { get; set; }

        [SuppressMessage("Microsoft.Usage",
             "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }
    }
}