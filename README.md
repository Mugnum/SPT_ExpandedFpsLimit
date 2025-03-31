# ExpandedFpsLimit
[Mod page.](https://hub.sp-tarkov.com/files/file/2750-expanded-fps-limit/) Game version: 3.10.5+.

[Download.](https://github.com/syrtsevser/SPT_ExpandedFpsLimit/releases)

![alt text](https://raw.githubusercontent.com/syrtsevser/SPT_ExpandedFpsLimit/refs/heads/master/Media/preview.png)

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
