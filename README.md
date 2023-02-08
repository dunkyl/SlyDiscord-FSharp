# ![sly logo](https://raw.githubusercontent.com/dunkyl/SlyMeta/main/sly%20logo%20f%23.svg) Sly Discord for F#

> 🚧 **This library is an early work in progress! Breaking changes may be frequent.**

> 🟣 for .NET 7+

## No boilerplate, _async_ and _typed_ Discord REST API access. 😋

```shell
dotnet add package net.dunkyl.SlyDiscord
```

This library is in a minimal implentation state
Currently, the following endpoints are supported:

 - users/@me

---

Example usage:

```fsharp
open net.dunkyl.SlyAPI
open net.dunkyl.SlyDiscord

let auth = OAuth2("app.json", "user.json")

let discord = Discord(auth)

printfn $"{discord.Me().Username}" // Dunkyl 🔣🔣
```