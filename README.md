# KyngNoodle-Cursed_Scrap_Mod

[![Build](https://github.com/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod/actions/workflows/build.yml/badge.svg)](https://github.com/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod/actions/workflows/build.yml)
[![Release](https://img.shields.io/github/v/release/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod)](https://github.com/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod/releases)

A mod for Lethal Company that allows only cursed scrap to spawn.

## Changelog

See the [Changelog](https://github.com/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod/blob/main/Changelog.md)

## Releases

Releases are published on [GitHub](https://github.com/KyngNoodle/Lethal-Company-Cursed-Scrap-Mod/releases) 
and [Thunderstore](https://thunderstore.io).

## Contributing

Create `CursedScrap/CursedScrap.csproj.user`:
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <LETHAL_COMPANY_DIR>C:/Program Files (x86)/Steam/steamapps/common/Lethal Company</LETHAL_COMPANY_DIR>
        <TEST_PROFILE_DIR>$(APPDATA)\r2modmanPlus-local\LethalCompany\profiles\Test Cursed Scrap Mod</TEST_PROFILE_DIR>
        <PACK_THUNDERSTORE>true</PACK_THUNDERSTORE>
    </PropertyGroup>

    <!-- Create your 'Test Profile' using your modman of choice before enabling this. 
    Enable by setting the Condition attribute to "true". *nix users should switch out `copy` for `cp`. -->
    <Target Name="CopyToTestProfile" AfterTargets="PostBuildEvent" Condition="false">
        <MakeDir
                Directories="$(TEST_PROFILE_DIR)/BepInEx/plugins/KyngNoodle-Cursed_Scrap_Mod"
                Condition="Exists('$(TEST_PROFILE_DIR)') And !Exists('$(TEST_PROFILE_DIR)/BepInEx/plugins/KyngNoodle-Cursed_Scrap_Mod')"
        />
        <Exec Command="copy &quot;$(TargetPath)&quot; &quot;$(TEST_PROFILE_DIR)/BepInEx/plugins/KyngNoodle-Cursed_Scrap_Mod/&quot;" />
    </Target>
</Project>
```