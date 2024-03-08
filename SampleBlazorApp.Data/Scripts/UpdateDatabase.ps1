if (!$args[0]) {
	Write-Error 'Please provide the name of the migration to which you would like to update.'
	return
}

dotnet ef database update $args[0] -s "SampleBlazorApp.Web/SampleBlazorApp.Web.csproj" -p "SampleBlazorApp.Data/SampleBlazorApp.Data.csproj" -c SampleContext