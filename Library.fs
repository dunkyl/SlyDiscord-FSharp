module net.dunkyl.SlyDiscord

open System
open Microsoft.FSharp.Core.Option

open net.dunkyl.SlyAPI

type Snowflake = Int64
type Color = Int32

let private cdn resource id hash =
    $"https://cdn.discordapp.com/{resource}/{id}/{hash}.png"

[<AutoOpen>]
module Scopes =
    let [<Literal>] IDENTIFY = "identify"
    let [<Literal>] GUILDS = "guilds"
    
type User = {
    Id: Snowflake
    Username: string
    Discriminator: string
    /// Hash
    Avatar: string option 
    /// Hash
    Banner: string option 
    AccentColor: Color option
} with
    member this.AvatarUrl () = map (cdn "avatars" this.Id) this.Avatar
    member this.BannerUrl () = map (cdn "banners" this.Id) this.Banner
    /// The accent color as an HTML hex code like #ABCDEF
    member this.HexColor () = map (sprintf "#%06x") this.AccentColor

type PartialGuild = {
    Id: Snowflake
    Name: string
    Owner: bool
    Permissions: Int64
    Features: string Set
}

/// REST API client
type Discord (auth: OAuth2) =
    inherit WebAPI(auth)
    
    override _.BaseURL = Uri "https://discord.com/api/v10/"
    override _.UserAgent = "SlyDiscord-FSharp/0.1.0"
    
    /// The currently authorized user
    [<RequiresScopes(IDENTIFY)>]
    member this.Me (): User Call = this.Get "users/@me" ()

    /// Guilds that the current user is a member of
    [<RequiresScopes(GUILDS)>]
    member this.Guilds (): PartialGuild list Call = this.Get "users/@me/guilds" ()