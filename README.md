# ExpandedFpsLimit
[Mod page.](https://forge.sp-tarkov.com/mod/2066/expanded-fps-limit)

[Download.](https://github.com/Mugnum/SPT_ExpandedFpsLimit/releases) Game versions: 3.10-3.11, 4.0+.

![alt text](https://raw.githubusercontent.com/Mugnum/SPT_ExpandedFpsLimit/refs/heads/master/Media/preview.png)

# Build instructions for new versions
1. Clear existing assembly references.
2. Create "Dependencies" folder and copy into it following files:
   - BepInEx\core\BepInEx.dll
   - BepInEx\plugins\spt\spt-reflection.dll
   - EscapeFromTarkov_Data\Managed\Assembly-CSharp.dll
   - EscapeFromTarkov_Data\Managed\UnityEngine.dll
   - EscapeFromTarkov_Data\Managed\UnityEngine.CoreModule.dll
3. Add references to project and compile "Release" build.
4. Copy resulting .dll file from \bin\Release\ to \BepInEx\plugins\
