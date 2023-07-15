# SharpCoachPlugin for Rider and ReSharper
This is plugin with useful features for comfortable C# development using JetBrains Rider IDE

### Build status
[![CI](https://github.com/DeagleGross/SharpCoachPlugin/actions/workflows/dotnet.yml/badge.svg)](https://github.com/DeagleGross/SharpCoachPlugin/actions/workflows/dotnet.yml)

### Stats 
![Rating](https://img.shields.io/jetbrains/plugin/r/rating/17522)  
![Downloads](https://img.shields.io/jetbrains/plugin/d/17522)   
![Version](https://img.shields.io/jetbrains/plugin/v/17522)

### Features Supported
- [Generate Mapping code from type A to type B](./docs/features/Mapping.md)

### You cannot install plugin in your Rider? 
*shout-out to AliExpress colleagues :)*

JetBrains plugins publishing system is *... hard*   
To help identify missing published versions, please, help me by [submitting an issue](https://github.com/DeagleGross/SharpCoachPlugin/issues/new?assignees=DeagleGross&labels=rider-version-compatibility&template=plugin-not-compatible-with-rider.md&title=Plugin+not+compatible+with+Rider+%3Cinsert-version-here%3E)  
I would be **very grateful** if you can create a PR by changing a couple of files, and help me publish new versions faster.

### Build
```
# For Rider
gradlew :runIde
```

### Publish
There are different entities that are needed to be changed for each publishing:
- `ProductVersion` specifies version of Rider
- `build` is a specific build in a product version
- `plugin_version` is a version of plugin, that is maintained by `SharpCoachPlugin` developers

##### Branches
All changes are firstly merged into `main` branch.
When a new plugin version is ready to be released, publisher has to merge `main` into `versions\<product_version>` branch.
Please, make sure that [plugin.xml](src/rider/main/resources/META-INF/plugin.xml) xml-attribute `<idea-version since-build="..." until-build="....*" />` sets all builds supported compatibility range (except some ridiculous situations). 

When `main` is merged in some specific branch, execute this command for publishing plugin and make sure it appears on `versions` page at [JetBrains.Marketplace.PluginPage](https://plugins.jetbrains.com/plugin/17522-coachsharp/versions/)

**It is not needed** to explicitly change `plugin_version` when publishing plugin using this command, because it is automatically replaced with an command line argument 

##### Versioning
[View JetBrains Rider ProductVersion & BuildNumbers](https://www.jetbrains.com/rider/download/other.html)

- *Major version* is updated when some serious features are released
- *Minor version* is updated on every new version publish
- *Hotfix version* is changed for every compatibility different version of plugin. (I.e. `1.0.1` supports `2021.1.3` ProductVersion and `1.0.2` supports `2021.1.5` ProductVersion)

```
# For Rider & ReSharper (Gradle)
gradlew :publishPlugin -PPluginVersion=<version> -PPublishToken=<token>
```

#### Supported ProductVersions and builds:
- branch `versions\2021.1.3` -> *ProductVersion* = 2021.1.3, *build* = 211.*
- branch `versions\2021.1.5` -> *ProductVersion* = 2021.1.5, *build* = 211.*
- branch `versions\2021.2` ->   *ProductVersion* = 2021.2, *build* = 212.*
- branch `versions\2021.2.1` -> *ProductVersion* = 2021.2.1, *build* = 212.*
- branch `versions\2021.2.2` -> *ProductVersion* = 2021.2.2, *build* = 212.*
- branch `versions\2023.3-223build` -> *ProductVersion* = 2023.3-SNAPSHOT, *build* = 223.*
- branch `versions\2023.1` -> *ProductVersion* = 2023.1, *build* = 231.*

### Contribution Info
Feel free to take part in developing the project. 
You can start with viewing opened [issues](https://github.com/DeagleGross/SharpCoachPlugin/issues).  
Also if you find any bugs or you have any ideas about new features, please, open a new issue
