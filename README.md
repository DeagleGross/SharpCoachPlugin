# SharpCoachPlugin for Rider and ReSharper

### Build status
[![CI](https://github.com/DeagleGross/SharpCoachPlugin/actions/workflows/dotnet.yml/badge.svg)](https://github.com/DeagleGross/SharpCoachPlugin/actions/workflows/dotnet.yml)

### Stats 
![Rating](https://img.shields.io/jetbrains/plugin/r/rating/17522)  
![Downloads](https://img.shields.io/jetbrains/plugin/d/17522)   
![Version](https://img.shields.io/jetbrains/plugin/v/17522)

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