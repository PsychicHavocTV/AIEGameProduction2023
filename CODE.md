# Coding Conventions
These are the **guidelines** to how scripts should be written and formatted in this project.

## Table of Contents
1. [Code Formatting](##code-formatting)
2. [Naming Conventions](##naming-conventions)
3. [Code File Layout](##code-file-layout)
4. [Code Documentation](##code-documentation)

## Code Formatting
- Use tabs instead of spaces, don’t mix them. 
- Each brace should have its own line, e.g.:
    ```csharp
    // BAD
    if (condition) {
        // Do something.
    }
    ```
    ```csharp
    // GOOD
    if (condition)
    {
        // Do something.
    }
    ```
    ```csharp
    // BAD
    public float Property { get { return field; } }
    ```
    ```csharp
    // GOOD
    public float Property
    {
        get
        {
            return field;
        }
    }
    ```
- It is fine to use the ‘=>’ operator for properties, e.g.: 
    ```csharp
    // GOOD
    public float Property
    {
        get => field;
        set => field = value;
    }
    ```
- Switch-case code should be implemented inside braces, e.g.: 
    ```csharp
    switch (condition)
    {
        case myCase:
        {
            // Do something...
        }
        break;
        default:
        {
            // Do something...
        }
        break;
    }
    ```

## Naming Conventions
- Classes, methods, namespaces, enums, properties, attributes, and coroutines should be named using “PascalCase”. 
    ```csharp
    namespace MyGame.MyNamespace
    public class MyClass
    public float MyProperty
    public void MyMethod()
    ```
- Fields, local variables, parameters should be named using “camelCase”. 
    ```csharp
    private int m_myField;
    float myVariable;
    MyMethod(int _myParameter);
    ```
- Constants should be named using “UPPER_CASE”. 
    ```csharp
    public const int MY_CONST
    ```
- Modifiers such as ‘public’, ‘private’, ‘protected’, ‘internal’, ‘static’, or ‘readonly’ ignore these casing conventions. 
- The naming of namespaces should be kept brief and describe the systems or definitions contained within. 
- The naming of classes and interfaces should be kept brief and describe its functionality, purpose, or data. 
- Abstract classes are preferred to be named with the suffix “Base”. 
    ```csharp
    abstract class AnimalBase
    public class Dog : AnimalBase
    ```
- Interfaces should include the prefix “I”. 
    ```csharp
    interface IMyInterface
    public class MyClass : IMyInterface
    ```
- Fields should include the prefix “m_”. 
- Method parameters should include the prefix “_”. 
- Only one class should be defined per file. 
- File name should be the same as the class name. 

## Code File Layout
- “using” directives should be at the top of a file. 
- “using” directives should be in the following order: 
    1. System or .NET libraries 
    2. Unity libraries 
    3. Third-Party libraries 
    4. Project libraries/utilities 
    5. Project Namespaces 
    6. Type Name Aliases 
- Classes should be defined in the following order: 
    1. Nested Classes 
    2. Constants 
    3. Enums 
    4. Properties 
    5. Fields 
    6. Constructors 
    7. Unity Messages 
    8. Public Methods 
    9. Private Methods 
    ```csharp
    public class MyClass : MonoBehaviour
    {
        private class MyNestedClass
        {
            // Stuff here...
        }
        
        private const int MY_CONST = 1;
        
        public enum MyEnum
        {
            First,
            Second
        }
        
        public int MyProperty
        {
            get => m_myField;
        }
        
        private int m_myField;
        
        private void Start()
        {
            // Do stuff...
        }
        
        public void MyMethod()
        {
            // Do stuff...
        }
        
        private void MyPrivateMethod(int _myParameter)
        {
            // Do stuff...
        }
    }
    ```

## Code Documentation
- Write “self-documenting” code when possible, avoiding overly abbreviated variables which aren’t immediately easy to understand and read. 
    ```csharp
    // BAD
    p += v * s * dt;
    ```
    ```csharp
    // GOOD
    position += velocity * speed * deltaTime;
    ```
- Write useful comments and documentation to help readability and allow the reader to understand, though avoid redundancy. 
- Avoid writing comments that contradict the code. 
