# Injectable.NetCore.Extensions
> Injectable Extensions enable simple, convention-based injection for .Net Core and .Net Standard services.

Injectable Extensions streamlines dependency injection in .Net Core and .Net Standard development scenarios by providing a set of extensions methods which enable convention-based injection from loaded assemblies. The extensions can be used directly from any IServiceCollection, or a broader set of injection rules can be provided via the InjectionSettings class.

## Installation


From Package Manager Console:
```sh
Install-Package Injectable.NetCore.Extensions
```

From Cli:
```sh
dotnet add package Injectable.NetCore.Extensions
```

## Injection Scopes
Injectable Extensions supports three forms of injection scopes provided by the `InjectionMode` enum:

```C#
namespace Injectable.NetCore.Extensions
{
    public enum InjectionMode
    {
        Scoped,
        Singleton,
        Transient
    }
}
```

## InjectionSettings
This is the definition of the `IInjectionSettings` interface. Any implementation of this interface can be passed into the `InjectByConvention` method.

| Property | Type | Default |  Usage |
| ------------------ |  ---------------- | ---------------- | ---------------- |
| InterfacePrefix | string | _null_ | Prefix of interface types, defaults to "I" |
| InterfaceSuffixList | List&lt;string&gt; | | Interface suffixs to identify injectables |
| InterfaceRootNamespaces | List&lt;string&gt; | |  Namespaces to load interfaces from |
| EnforceStrictNaming | bool | _true_ | When set to true, Injected classes must have the same name as the interface, without the leading prefix <br /><br />_For Example: INameProvider will inject the NameProvider class, but not TheNameProvider, even if TheNameProvider implements INameProvider_ |
| RestrictImplementationsToInterfaceNamespaces | bool | _false_ | When set to true, only classes in the same root namespaces as the interface collections they implement will be injected |
| InjectionMode | InjectionMode | _InjectionMode.Scoped_ | Specifies the injection mode from `Injectable.NetCore.Extensions.InjectionMode`|

| Method | Returns | Usage |
| --- | --- | --- |
| Validate() | _void_ | Checks settings for minimum viable usability, throws an InvalidOperationException if validation criteria fails |
| Configure() | `IInjectionRootNamespaceConfiguration` | Begins Fluent Configuration |

## Usage 1: Fluent Injection

There is a fluent method for configuring the `IInjectionSettings` in the package that will guide you through the completion of valid settings. 

If your interface declarations are:
```C#
namespace MyNamespace.ServiceContracts {

    public interface IPersonService {}
    public interface IAddressService {}
}
namespace MyUfoNamespace {
    public interface IUfoService {}
}
```
By convention, your implementations are:
```C#
namespace MyOtherNamespace.Services {

    public class PersonService: IPersonService {}
    public class AddressService: IAddressService {}
    public class UfoService: IUfoService {}
}
```

This method of injection will look similar to this:

```C#
public void ConfigureServices(IServiceCollection services){
    
    var injectionSettings = InjectionSettings
        .WithInjectionMode(InjectionMode.Scoped)
        .WithRootNamespaces("MyNamespace", "MyUfoNamespace")
        .AllowImplementationsInAnyNamespace()
        .WithInterfacePrefix("I")
        .WithInterfaceSuffix("Service")
        .WithStrictNaming();

    services.InjectByConvention(injectionSettings);
}
```
This will result in All Interfaces starting with "I" and ending in "Service" residing anywhere in the InjectionClasses or InjectionClasses2 namespaces with implementations whose class names match the interface names (without the prefix). Since multiple root namespaces were specified, all the implementations will be injected.

## Usage 2: Explicit Scope Injection

Explicit extension methods exist for each type of scope by providing a root namespace and suffix. So if my convention for naming your service interfaces is "I&lt;ClasasName&gt;Service" (we default the prefix to "I", but that can also be overriden)

If your interface declarations are:
```C#
namespace MyNamespace.ServiceContracts {

    public interface IPersonService {}
    public interface IAddressService {}
    public interface IUfoService {}
}
```
By convention, your implementations are:
```C#
namespace MyOtherNamespace.Services {

    public class PersonService: IPersonService {}
    public class AddressService: IAddressService {}
    public class UfoService: IUfoService {}
}
```
Then injecting those into your services is as easy as: 

```C#
public void ConfigureServices (IServiceCollection services) {

    // Transient injection
    services.InjectTransientsFrom("MyNamespace", "Services");
    // Scoped injection
    services.InjectScopedFrom("MyNamespace", "Services");
    // Singleton injection
    services.InjectSingletonsFrom("MyNamespace", "Services");
}
```

There are many optional parameters to the Inject extenstions that allow you to customize the behavior which all roll up into an InjectionSettings instance before being passed into the `InjectByConvention` method. 

## Reflection, Injection, and Multiple Implementations

I realize there is nothing that prevents or restricts the implementation of an interface on multiple classes. HOWEVER, there is a limitation on this imposed by the dependency injection process that does not allow multiple implementations for injected types. This will cause an error.
