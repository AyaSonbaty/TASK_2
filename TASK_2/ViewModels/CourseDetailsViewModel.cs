namespace TASK_2.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MinDegree { get; set; }
        public string DepartmentName { get; set; }
        public List<string> InstructorNames { get; set; } = new List<string>();


    }
}
