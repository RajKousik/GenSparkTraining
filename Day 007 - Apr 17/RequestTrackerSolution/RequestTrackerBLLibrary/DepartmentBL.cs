using RequestTrackerDALLibrary;
using RequestTrackerDALLibrary.Model;
//using RequestTrackerModelLibrary;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace RequestTrackerBLLibrary
{
    public class DepartmentBL : IDepartmentService
    {
        readonly IRepository<int, Department> _departmentRepository;

        public DepartmentBL()
        {
            _departmentRepository = new DepartmentRepository();
        }

        public DepartmentBL(IRepository<int, Department> departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public int? AddDepartment(Department department)
        {
            var result = _departmentRepository.Add(department);
            if (result != null)
            {
                return result.Id;
            }
            throw new DuplicateDepartmentNameException();
        }

        public Department ChangeDepartmentName(string departmentOldName, string departmentNewName)
        {
            var department = _departmentRepository.GetAll().Find(d => d.Name == departmentOldName);
            if (department == null)
            {
                // Department with old name not found
                throw new DepartmentNotFoundException();
            }

            // Check if the new name already exists
            if (_departmentRepository.GetAll().Exists(d => d.Name == departmentNewName))
            {
                throw new DuplicateDepartmentNameException();
            }

            // Update department name
            department.Name = departmentNewName;
            return _departmentRepository.Update(department);
        }

        public bool DeleteDepartment(int id)
        {
            var deletedDepartment = _departmentRepository.Delete(id);
            if (deletedDepartment != null)
            {
                return true;
            }
            throw new DepartmentNotFoundException();
        }

        public Department GetDepartmentById(int id)
        {
            var result = _departmentRepository.Get(id);
            if (result != null)
            {
                return result;
            }
            throw new DepartmentNotFoundException();

        }

        public Department GetDepartmentByName(string departmentName)
        {
            var department = _departmentRepository.GetAll().Find(d => d.Name == departmentName);
            if (department == null)
            {
                // Department with old name not found
                throw new DepartmentNotFoundException();
            }
            return department;
        }

        [ExcludeFromCodeCoverage]
        public int? GetDepartmentHeadId(int departmentId)
        {
            var department = _departmentRepository.Get(departmentId);
            if (department != null)
            {
                return department.DepartmentHead;
            }
            throw new DepartmentNotFoundException();

        }

        public List<Department> GetDepartmentList()
        {
            var departments = _departmentRepository.GetAll();
            if(departments == null)
            {
                throw new DepartmentNotFoundException();
            }
            return departments;
        }

    }
}
