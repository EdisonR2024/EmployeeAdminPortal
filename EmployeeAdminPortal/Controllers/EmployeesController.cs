using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    //localhost:####/api/employees
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpGet("{id}")]
        public ActionResult GetEmployeeById(Guid id)
        {           
            var employee = _dbContext.Employees.Find(id);

            if (employee is null) return NotFound("Empleado no encontrado");

            return Ok(employee);
        }

        [HttpPost]
        public ActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employeeEntity = new Employee()
            {
                //Id = Guid.NewGuid(),//No es necesario, ya que EF Core lo genera automáticamente
                Name = addEmployeeDto.FirstName + " " + addEmployeeDto.LastName,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary
            };

            _dbContext.Employees.Add(employeeEntity);
            _dbContext.SaveChanges();
            //return Ok("Se ha creado un nuevo empleado");
            //return Created();
            //return Ok(employeeEntity);
            //Console.WriteLine("Id del empleado nuevo: " + employeeEntity.Id); // Para ver el Id generado por EF Core
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeEntity.Id }, employeeEntity);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(Guid id, AddEmployeeDto updateEmployee) {
                        
            var existingEmployee = _dbContext.Employees.Find(id);

            if (existingEmployee is null )
            {
                return NotFound("Empleado no existe");
            }

            existingEmployee.Name = updateEmployee.FirstName + " " + updateEmployee.LastName;
            existingEmployee.Email = updateEmployee.Email;
            existingEmployee.Phone = updateEmployee.Phone;
            existingEmployee.Salary = updateEmployee.Salary;
            
            //_dbContext.Employees.Update(existingEmployee); // Esta linea es opcional, ya que Find ya devuelve una entidad en estado "Unchanged" que significa que no ha sido modificada.
            _dbContext.SaveChanges();

            return Ok("Empleado actualizado correctamente");

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(Guid id) {
            var existingEmployee = _dbContext.Employees.Find(id);
            if (existingEmployee is null)
            {
                return NotFound("Empleado no existe");
            }

            _dbContext.Employees.Remove(existingEmployee);
            _dbContext.SaveChanges();

            return Ok("Empleado eliminado correctamente");
        }

    }
}
