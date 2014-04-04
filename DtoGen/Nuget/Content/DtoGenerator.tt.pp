<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ assembly name="System.Core" #>
<#@ assembly name="$(TargetDir)DtoGen.Core.dll" #>
<#@ assembly name="$(TargetPath)" #>
<#@ import namespace="DtoGen.Core" #>
<#@ import namespace="$rootnamespace$.DTOs" #>
<#@ output extension=".cs" #>

<#= 
    // See https://github.com/tomasherceg/dto-gen for more information.   
	
	// This generates the UserData DTO from the sample User class DTO
		
	Transform
	    .Create<User, UserData>()
	    .Generate() 
	
#>