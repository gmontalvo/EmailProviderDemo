# Email Provider Demo


## Build Instructions

1. git clone https://github.com/gmontalvo/EmailProviderDemo.git
2. git clone https://github.com/salesforce-marketingcloud/FuelSDK-CSharp.git
3. git clone https://github.com/sendgrid/sendgrid-csharp.git
4. Open SendGrid visual studio solution (Navigate to the directory sendgrid-csharp)
5. Build the "Mail" Project only (will get some required nuget packages)
6. Open EmailProviderDemo visual studio solution
7. Fix broken references in SendGrid Project (Delete then re-add)
8. Build EmailProviderDemo solution
9. Add api keys to App.config for EmailProviderDemo project