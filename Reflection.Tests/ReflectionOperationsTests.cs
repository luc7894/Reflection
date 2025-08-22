using System.Numerics;
using System.Reflection;
using NUnit.Framework;
using Reflection.Tests.TypesForTesting;

[assembly: CLSCompliant(true)]

namespace Reflection.Tests
{

    [TestFixture]
    public class ReflectionOperationsTests
    {
        private static readonly object[][] GetTypeNameData = new object[][]
        {
        [new ClassForTesting(), "ClassForTesting"],
        [new Employee(101, "Rahul", 22, "JSE", 600000, 45000), "Employee"],
        };

        private static readonly object[][] GetFullTypeNameData = new object[][]
        {
        [typeof(ClassForTesting), "Reflection.Tests.TypesForTesting.ClassForTesting"],
        [typeof(Employee), "Reflection.Tests.TypesForTesting.Employee"],
        };

        private static readonly object[][] GetAssemblyQualifiedNameData = new object[][]
        {
        [
            typeof(ClassForTesting),
            "Reflection.Tests.TypesForTesting.ClassForTesting, Reflection.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        ],
        [
            typeof(Employee),
            "Reflection.Tests.TypesForTesting.Employee, Reflection.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
        ],
        };

        private static readonly object[][] GetPrivateInstanceFieldsData = new object[][]
        {
        [new ClassForTesting(), new string[] { "intField" }],
        [
            new Employee(101, "Rahul", 22, "JSE", 600000, 45000),
            new string[]
            {
                "<Id>k__BackingField", "<Name>k__BackingField", "<Age>k__BackingField", "<Role>k__BackingField",
                "<AnnualSalary>k__BackingField", "<SalaryPerYear>k__BackingField",
            }
        ],
        };

        private static readonly object[][] GetPublicStaticFieldsData = new object[][]
        {
        [new ClassForTesting(), new string[] { "StaticIntField" }],
        [
            new Employee(101, "Rahul", 22, "JSE", 600000, 45000), Array.Empty<string>()
        ],
        };

        private static readonly object[][] GetInterfaceData = new object[][]
        {
        [new Employee(103, "Suresh", 23, "SE", 700000, 55000), new string[] { "Reflection.Tests.TypesForTesting.IEmployee" }],
        };

        private static readonly object[][] GetConstructorsData = new object[][]
        {
        [
            new Employee(104, "Salman", 24, "SE", 700000, 55000),
            new string[] { "Void .ctor(Int32, System.String, Int32, System.String, Int32, Int32)" }
        ],
        };

        private static readonly object[][] GetTypeMembersData = new object[][]
        {
        [
            new Employee(105, "Chris", 27, "SSE", 1000000, 100000),
            new string[]
            {
                "Int32 get_Id()", "Void set_Id(Int32)", "System.String get_Name()", "Void set_Name(System.String)",
                "Int32 get_Age()", "Void set_Age(Int32)", "System.String get_Role()", "Void set_Role(System.String)",
                "Int32 get_AnnualSalary()", "Void set_AnnualSalary(Int32)", "Int32 get_SalaryPerYear()",
                "Void set_SalaryPerYear(Int32)", "System.String GetDetails()", "Int32 GetSalaryPerMonth()",
                "System.String GetOrganizationName()", "Boolean Equals(System.Object)", "Int32 GetHashCode()",
                "System.Type GetType()", "System.String ToString()",
                "Void .ctor(Int32, System.String, Int32, System.String, Int32, Int32)", "Int32 Id",
                "System.String Name", "Int32 Age", "System.String Role", "Int32 AnnualSalary", "Int32 SalaryPerYear",
            }
        ],
        };

        private static readonly object[][] GetMethodData = new object[][]
        {
        [
            new Employee(106, "John", 25, "SSE", 1000000, 100000),
            new string[]
            {
                "Int32 get_Id()", "Void set_Id(Int32)", "System.String get_Name()", "Void set_Name(System.String)",
                "Int32 get_Age()", "Void set_Age(Int32)", "System.String get_Role()", "Void set_Role(System.String)",
                "Int32 get_AnnualSalary()", "Void set_AnnualSalary(Int32)", "Int32 get_SalaryPerYear()",
                "Void set_SalaryPerYear(Int32)", "System.String GetDetails()", "Int32 GetSalaryPerMonth()",
                "System.String GetOrganizationName()", "Boolean Equals(System.Object)", "Int32 GetHashCode()",
                "System.Type GetType()", "System.String ToString()",
            }
        ],
        };

        private static readonly object[][] GetPropertiesData = new object[][]
        {
        [
            new Employee(108, "Vivek", 24, "JSE", 600000, 45000),
            new string[]
            {
                "Int32 Id", "System.String Name", "Int32 Age", "System.String Role", "Int32 AnnualSalary",
                "Int32 SalaryPerYear",
            }
        ],
        };

        //
        public static string GetTypeName(object obj) //vrijedi samo za prvi test case
        {
            Type type = obj.GetType();
            return type.Name;
        }


        [TestCaseSource(nameof(GetTypeNameData))]
        public void GetTypeName_ReturnsTypeName(object obj, string expected)
        {
            // Act
            string actual = ReflectionOperations.GetTypeName(obj); // <---- ovo je metoda koja je falila da se zalijepi

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        //
        public static string GetFullTypeName<T>() //drugi test case, genericka metoda
        {
            Type type = typeof(T);
            return type.FullName!;
        }


        [TestCaseSource(nameof(GetFullTypeNameData))]
        public void GetFullTypeName_ReturnsFullTypeName(Type type, string expected)
        {
            // Arrange
            MethodInfo? methodInfo = typeof(ReflectionOperations).GetMethod(
                nameof(ReflectionOperations.GetFullTypeName),
                BindingFlags.Static | BindingFlags.Public);
            MethodInfo genericMethodInfo = methodInfo!.MakeGenericMethod(type); //zbog ovo je genericka metoda

            // Act
            object? actual = genericMethodInfo.Invoke(null, null);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        //

        public static string GetAssemblyQualifiedName<T>() //treci test case, genericka metoda
        {
            Type type = typeof(T);
            return type.AssemblyQualifiedName!;
        }


        [TestCaseSource(nameof(GetAssemblyQualifiedNameData))]
        public void GetAssemblyQualifiedName_ReturnsAssemblyQualifiedName(Type type, string expected)
        {
            // Arrange
            MethodInfo? methodInfo = typeof(ReflectionOperations).GetMethod(
                nameof(ReflectionOperations.GetAssemblyQualifiedName),
                BindingFlags.Static | BindingFlags.Public);
            MethodInfo genericMethodInfo = methodInfo!.MakeGenericMethod(type);

            // Act
            object? actual = genericMethodInfo.Invoke(null, null);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        //
        public static string[] GetPrivateInstanceFields(object obj) //cetvrti test case
        {
            Type type = obj.GetType(); //prvo dobiti tip objekta
            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            //binding flags treba paziti koje uzima sve instance ali privatne fieldove
            return fields.Select(field => field.Name).ToArray(); //i vratiti samo imena tih fieldova
        }




        [TestCaseSource(nameof(GetPrivateInstanceFieldsData))]
        public void GetPrivateInstanceFields(object obj, string[] expected)
        {
            // Act
            string[] actual = ReflectionOperations.GetPrivateInstanceFields(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        public static string[] GetPublicStaticFields(object obj) //peti test case
        {
           Type type= obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            return fields.Select(field => field.Name).ToArray();
        }


        [TestCaseSource(nameof(GetPublicStaticFieldsData))]
        public void GetPublicStaticFields(object obj, string[] expected)
        {
            // Act
            string[] actual = ReflectionOperations.GetPublicStaticFields(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }


        public static string?[] GetInterfaceDataDetails(object obj) //sesti test case
        {
            Type type=obj.GetType();
            Type[] interfaces = type.GetInterfaces();
            return interfaces.Select(i => i.FullName).ToArray();

            //inteface je type polje jer je to tip, a feildovi nije tip vec CLAN klase
        }




        [TestCaseSource(nameof(GetInterfaceData))]
        public void GetInterfaceDataDetails(object obj, string[] expected)
        {
            // Act
            string?[] actual = ReflectionOperations.GetInterfaceDataDetails(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }


        //

        public static string?[] GetConstructorsDataDetails(object obj) //sedmi test case
        {
           Type type= obj.GetType();
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.Select(c => c.ToString()).ToArray();

            //konstrutor nije tip vec clan klase

        }



        [TestCaseSource(nameof(GetConstructorsData))]
        public void GetConstructorsDataDetails(object obj, string[] expected)
        {
            // Act
            string?[] actual = ReflectionOperations.GetConstructorsDataDetails(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        //

        public static string?[] GetTypeMembersDataDetails(object obj) //osmi test case
        {
            Type type = obj.GetType();
            MemberInfo[] members = type.GetMembers();
            return members.Select(m => m is MethodInfo method
            ? $"{method.ReturnType.Name} {method.Name}({string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})" : m.Name)
                .ToArray();

            //member su konstruktori, polja, objekti, naslijedene metode, property itd znaci sve treba iz klase uzet
        }

        [TestCaseSource(nameof(GetTypeMembersData))]
        public void GetTypeMembersDataDetails(object obj, string[] expected)
        {
            // Act
            string?[] actual = ReflectionOperations.GetTypeMembersDataDetails(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        //

        public static string?[] GetMethodDataDetails(object obj) //deveti test case
        {
            Type type = obj.GetType();
            MethodInfo[] methods= type.GetMethods();
            return methods.Select(m => $"{m.ReturnType.Name}" +
            $" {m.Name}({string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})").ToArray();
        }


        [TestCaseSource(nameof(GetMethodData))]
        public void GetMethodDataDetails(object obj, string[] expected)
        {
            // Act
            string?[] actual = ReflectionOperations.GetMethodDataDetails(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        //deseti test case
        public static string?[] GetPropertiesDataDetails(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            return properties.Select(p => $"{p.PropertyType.Name} {p.Name}").ToArray();
        }


        [TestCaseSource(nameof(GetPropertiesData))]
        public void GetPropertiesDataDetails(object obj, string[] expected)
        {
            // Act
            string?[] actual = ReflectionOperations.GetPropertiesDataDetails(obj);

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }
    }
}
//u read me ima pomoc
//samo sam trebala paste GetType metodu od 1 koraka
//ta metoda se ne poziva rucno vec
//NUnit automatski poziva tu metodu kroz testove, koristi podatke iz GetTypeNameData i provjerava rezultat.
//NUnit je biblioteka koja zna “čitati” tvoj kod i tražiti metode označene s [Test] ili [TestCaseSource(...)].

//u ovom 2d nizu uzima nakon zareza
/*private static readonly object[][] GetTypeNameData = new object[][]
{
    [new ClassForTesting(), "ClassForTesting"],[new Employee(101, "Rahul", 22, "JSE", 600000, 45000), "Employee"],
};*/

//sprema u ovu metodu public void GetTypeName_ReturnsTypeName(object obj, string expected)
// za prvi ce biti obj= newClassForTesting(), expected="ClassForTesting"
//onda ulazi tu string actual = ReflectionOperations.GetTypeName(obj);
//te tamo trazi kojeg je tipa taj objekt i njegovo ime to tipa vraca i usporeduje jel jednako expected

/*namespace MyApp.Models
{
    public class Employee { }
}
typeof(Employee).Name → Employee
Samo ime klase, bez namespacea.

typeof(Employee).FullName → MyApp.Models.Employee
Puno ime klase uključujući namespace.

Dakle, FullName je “potpuni put” do tipa, dok Name je samo ime klase.*/

/*public static string GetFullTypeName(Type type)
{
    return type.FullName!;
}
Tada samo pozoveš:

csharp
Copy
Edit
string result = ReflectionOperations.GetFullTypeName(typeof(Employee));
ovako bi fullname izgledao da nije pozvano da bude genericka metoda
 */
