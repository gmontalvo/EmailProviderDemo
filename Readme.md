# Email Provider Demo

# Background

Tasked with developing an application that demonstrates API usage across several email service providers, I was disappointed at the dirth of availability of such code online.  I decided to put together a simple .NET application that demonstrates the following:
1. Create an email, and send it to a list of recipients
2. Query back statistics for the email send

Since several vendors are involved, I coded to the least common denominator.  The whole purpose of the app, however, is to demonstrate the easiest use case - and not to demonstrate the full array of features for any specific vendor.

Final note ... since several vendors offer their SDK in GitHub, I used the source (vs. NuGet) in the project.  As a result, the build is a bit wonky, but it works on my machine!  ;-)

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