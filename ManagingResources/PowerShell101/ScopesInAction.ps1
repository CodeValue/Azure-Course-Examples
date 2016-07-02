Function FunctionScope
{
    'Changing $MyVar with a function.'
    $MyVar = 'I got set by a function!'
    "MyVar says $MyVar"
}
''
'Checking current value of $MyVar.'
"MyVar says $MyVar"
''
'Changing $MyVar by script.'
$MyVar = 'I got set by a script!'
"MyVar says $MyVar"
''
FunctionScope
''
'Checking final value of MyVar before script exit.'
"MyVar says $MyVar"
''
'=================================================================='
'second example: working with scope modifiers'
'=================================================================='

Function FunctionScopeWithModifiers
{
    ''
    'Changing $MyVar in the local function scope...'
    $local:MyVar = "This is MyVar in the function's local scope."
    'Changing $MyVar in the script scope...'
    $script:MyVar = 'MyVar used to be set by a script. Now set by a function.'
    'Changing $MyVar in the global scope...'
    $global:MyVar = 'MyVar was set in the global scope. Now set by a function.'
    ''
    'Checking $MyVar in each scope...'
    "Local: $local:MyVar"
    "Script: $script:MyVar"
    "Global: $global:MyVar"
    ''
}
''
'Getting current value of $MyVar.'
"MyVar says $MyVar"
''
'Changing $MyVar by script.'
$MyVar = 'I got set by a script!'
"MyVar says $MyVar"

FunctionScopeWithModifiers

'Checking $MyVar from script scope before exit.'
"MyVar says $MyVar"
''

'=================================================================='
'third example: working with scope numbers'
'=================================================================='

Function FunctionScopeWithScopeNumbers
{    
    ''
    'Changing $MyVar in scope 0, relative to FunctionScope...'
    Set-Variable MyVar "This is MyVar in the function's scope 0." -Scope 0
    'Changing $MyVar in scope 1, relative to FunctionScope...'
    Set-Variable MyVar 'MyVar was changed in scope 1, from a function.' -Scope 1
    'Changing $MyVar in scope 2, relative to Functionscope...'
    Set-Variable MyVar 'MyVar was changed in scope 2, from a function.' -Scope 2
    ''
    'Checking $MyVar in each scope...'
    'Scope 0:'
    Get-Variable MyVar -Scope 0 -ValueOnly
    'Scope 1:'
    Get-Variable MyVar -Scope 1 -ValueOnly
    'Scope 2:'
    Get-Variable MyVar -Scope 2 -ValueOnly
    ''
}
''
'Getting current value of $MyVar.'
"MyVar says $MyVar"
''
'Changing $MyVar by script.'
$MyVar = 'I got set by a script!'
"MyVar says $MyVar"

FunctionScopeWithScopeNumbers

'Checking $MyVar from script scope before exit.'
"MyVar says $MyVar"