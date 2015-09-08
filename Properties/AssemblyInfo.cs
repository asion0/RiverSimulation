using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 組件的一般資訊是由下列的屬性集控制。
// 變更這些屬性的值即可修改組件的相關
// 資訊。
[assembly: AssemblyTitle("RiverSimulationApplication")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RiverSimulationApplication")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 將 ComVisible 設定為 false 會使得這個組件中的型別
// 對 COM 元件而言為不可見。如果您需要從 COM 存取這個組件中
// 的型別，請在該型別上將 ComVisible 屬性設定為 true。
[assembly: ComVisible(false)]

// 下列 GUID 為專案公開 (Expose) 至 COM 時所要使用的 typelib ID
[assembly: Guid("4f28e954-34ce-410c-a79b-def203079b55")]

// 組件的版本資訊是由下列四項值構成:
//
//      主要版本
//      次要版本 
//      組建編號
//      修訂編號
//
// 您可以指定所有的值，也可以依照以下的方式，使用 '*' 將組建和修訂編號
// 指定為預設值:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.45")]
[assembly: AssemblyFileVersion("1.0.45")]

//1.0.0.45 - 20150908 Release version, Parsing Result.
//1.0.0.44 - 20150826 Release version, Parsing Result.
//1.0.0.43 - 20150810 Release version, Fixed 20150731 report.
//1.0.0.42 - 20150731 Release version, Modify input file format.
//1.0.0.41 - 20150629 Release version, Fix all bugs reported.
//1.0.0.40 - 20150511 Release version, Change some spec from 20150402.
//1.0.0.39 - 20150323 Release version, Add button in import form.
//1.0.0.38 - 20150323 Release version, Support Z data.
//1.0.0.37 - 20150317 Release version, Fix some bugs.
//1.0.0.36 - 20150313 Release version, Modify some error and add load project function.
//1.0.0.35 - 20150306 Release version, Modify some double value to string format.
//1.0.0.34 - 20150305 Release version, Fixed 0.ToString("###.###") null bug.
//1.0.0.33 - 20150302 Release version, Fixed 1.1.1.3 in constance flow type bug.
//1.0.0.32 - 20150301 Release version, Support constance flow type.
//1.0.0.31 - 20150223 Release version, Fixed input grid manual issue.
//1.0.0.30 - 20150221 Release version.
//1.0.0.29 - 20150218 Release version.
//1.0.0.28 - 20150217 Release version.
//1.0.0.27 - 20150207 Release version.
//1.0.0.26 - 20150206 Release version.
//1.0.0.25 - 20150205 Release version.
//1.0.0.24 - 20150122 Release version.
//1.0.0.23 - 20141224 Add ThreeWayTable class for BoundaryConditionsForm.
//1.0.0.22 - 20141224 Weekly build version, export input file.
//1.0.0.21 - 20141215 Weekly build version
//1.0.0.20 - 20141208 Weekly build version
//1.0.0.19 - 20141201 Weekly build version
//1.0.0.18 - Process some parameters in Movablebed
//1.0.0.17 - Demo version on 20141125
//1.0.0.16 - 修正20141124回報問題，關於結構物高程編輯
//1.0.0.15 - Release in 20141114 客製化第一階段.
//1.0.0.14 - Release in 20141121 for Customize request.
//1.0.0.13 - Release in 20141114 for import form and Movable Form.
//1.0.0.12 - Release in 20141110 for display multiple dry bed in select form.
//1.0.0.11 - Add GridPictureBox control
//1.0.0.10 - Fixed import image resulation issue.
//1.0.0.9 - Demo 匯入網格 in 20141018
//1.0.0.8 - Demo 模擬收斂圖 in 20141006
//1.0.0.7 - Modify for 20140916修改回應.pdf in 20140916
//1.0.0.6 - Modify for 20140827v3修改回應.pdf in 20140915
//1.0.0.5 - Add Lite Version
//1.0.0.4 - Modify UI by 20140801 word spec.
//1.0.0.3 - Modify more UI screens for 20140808 demo version
//1.0.0.2 - Modify more UI screens for 20140710 demo version
//1.0.0.1 - First release for demo UI.