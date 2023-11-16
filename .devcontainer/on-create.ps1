oh-my-posh font install CascadiaCode

New-Item -Path $PROFILE -Type File -Force
Write-Output 'oh-my-posh --init --shell pwsh --config "https://gist.githubusercontent.com/shanselman/1f69b28bfcc4f7716e49eb5bb34d7b2c/raw/8e9c9a8736ff4e9e5a863c20833d614549ccbc32/ohmyposhv3-v2.json" | Invoke-Expression' > $PROFILE