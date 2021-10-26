# SharpCoachPlugin for Rider and ReSharper

[![ReSharper](https://img.shields.io/jetbrains/plugin/v/17499?label=Sharp%20Coach%20Plugin&style=for-the-badge)](https://plugins.jetbrains.com/plugin/17522-coachsharp)  
![Downloads](https://img.shields.io/resharper/dt/:coachsharp?style=for-the-badge)  
![Version](https://img.shields.io/resharper/v/:coachsharp?label=version&style=for-the-badge)

### Branches
When new version of plugin is published, it is needed to merge `main` to all branches `versions\rider_***`.
In each of the versions `Product_Version` in [gradle.properties](./gradle.properties) is different, so that users can get plugin for different Rider build versions.

### Build
```
# For Rider
gradlew :runIde
```

### Publish
```
# For Rider & ReSharper (Gradle)
gradlew :publishPlugin -PPluginVersion=<version> -PPublishToken=<token>
```