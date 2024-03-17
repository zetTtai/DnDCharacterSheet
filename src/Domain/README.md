# Domain Layer

Business rules and logic.

## Folder Structure:

### Common &rarr; Shared items across the domain layer

- **BaseAuditableEntity.cs** &rarr; An abstract class that inherits from BaseEntity and includes attributes such as Created, CreatedBy, LastModified, and LastModifiedBy.
- **BaseEntity.cs** &rarr; An abstract class that includes an Id + List of BaseEvents.cs. Also includes functions to manipulate this list (Add, Remove, and Clear these **DomainEvents**).
- **BaseEvents.cs** &rarr; An abstract class that inherits from INotification (MediatR pub-sub manipulator, similar to Kafka).
- **ValueObject.cs** &rarr; An abstract class that implements Equals, GetHashCode, and NotEquals functions.

### Constants &rarr; Constant values domain-specific

- **Policies.cs** &rarr; Constant values related to policies, for example, "CanPurge".
- **Roles.cs** &rarr; Constant values related to roles, for example, "Administrator". Similar to enums but using strings instead.

### Entities &rarr; Business objects of the App

### Enums &rarr; Enumerations of named constants

### Events &rarr; Classes that inherit from BaseEvent.cs (MediatR)

The purpose of these classes is to NOTIFY (INotification) other parts of the application when they are triggered.
**Example**: TodoItemCompletedEvent.cs -> Event sent to DomainEvents (BaseEntity) indicating to the rest of the application that a specific TodoItem has changed its Done attribute to true.

### Exceptions &rarr; Custom exception domain-specific

### ValueObjects &rarr; Small Objects/Entities with no ID

A ValueObject is an object that is defined by its attributes and, once created, cannot change. For example, Money. When you compare two Money objects, you don't check if they are equal by their ids but by their attributes, like Currency + Quantity.
When we replace a ValueObject these change is not notified to the rest of the application.

# Entities vs ValueObjects

An Entity has a UNIQUE identity (ID) that doesn't change over time. An entity is not defined by its attributes but by its id in a thread of continuity. On the other hand, a ValueObject is defined by its attributes and is IMMUTABLE. When you need to "modify" a ValueObject, what you do is replace it with a new one. ValueObjects are completely replaceable.

# Enums vs Constants

**Enums** **group** related options for a variable. They make code clearer and prevent mistakes with wrong values.

**Constants** are unchanging **single** values used in many parts of the code, like settings or limits.

Use enums for choosing from specific options and constants for fixed values in your code.