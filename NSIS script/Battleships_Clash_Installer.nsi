Unicode True
!include "MUI2.nsh"
!include "FileFunc.nsh"
!define PUBLISHER_NAME "John James Gutib"
!define PRODUCT_NAME "Battleships Clash"
!define PRODUCT_VERSION "1.0.0.0"
!define OutputFileName "BattleshipsClash_Installer.exe"
!define ARP "Software\Microsoft\Windows\CurrentVersion\Uninstall\BattleshipsClash"
!define MUI_ICON "BattleshipsClash_Icon.ico"
!define MUI_ABORTWARNING

!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

Name "${PRODUCT_NAME}"
OutFile "${OutputFileName}"
InstallDir "$PROGRAMFILES64\${PRODUCT_NAME}"
RequestExecutionLevel admin

!define /file OutFileSignPassword ".\CodeSign\passwd.txt"
!define OutFileSignCertificate ".\CodeSign\certificate.pfx"
!define OutFileSignSHA1   ".\CodeSign\signtool.exe sign /f ${OutFileSignCertificate} /p ${OutFileSignPassword} /fd sha1   /t  http://timestamp.comodoca.com /v" 
!define OutFileSignSHA256 ".\CodeSign\signtool.exe sign /f ${OutFileSignCertificate} /p ${OutFileSignPassword} /fd sha256 /tr http://timestamp.comodoca.com?td=sha256 /td sha256 /as /v" 

/*
# Sign installer
!finalize "PING -n 1 127.0.0.1 >nul"
!finalize "${OutFileSignSHA1} .\${OutputFileName}"
!finalize "PING -n 5 127.0.0.1 >nul"
!finalize "${OutFileSignSHA256} .\${OutputFileName}"
*/

Section "install"
	# Install files
    SetOutPath "$INSTDIR\Battleships_Clash_Data"
	File /r "..\Build\Battleships_Clash_Data\"
	SetOutPath $INSTDIR
	File "..\Build\Battleships_Clash.exe"
	File "BattleshipsClash_Icon.ico"
 
    # Create the uninstaller
    WriteUninstaller "BattleshipsClash_Uninstaller.exe"
	
	# Calculate total size of files installed
	${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
	IntFmt $0 "0x%08X" $0
	
	# Write reg keys for uninstaller
	WriteRegStr HKLM "${ARP}" \
                 "DisplayName" "${PRODUCT_NAME}"
	WriteRegStr HKLM "${ARP}" \
                 "UninstallString" "$\"$INSTDIR\BattleshipsClash_Uninstaller.exe$\""
	WriteRegStr HKLM "${ARP}" \
                 "InstallLocation" "$\"$INSTDIR$\""
	WriteRegStr HKLM "${ARP}" \
                 "Publisher" "${PUBLISHER_NAME}"
	WriteRegStr HKLM "${ARP}" \
                 "DisplayVersion" "${PRODUCT_VERSION}"
	WriteRegStr HKLM "${ARP}" \
                 "DisplayIcon" "$\"$INSTDIR\BattleshipsClash_Icon.ico$\""
	WriteRegDWORD HKLM "${ARP}" "EstimatedSize" "$0"
	
	# Create shortcuts
	CreateDirectory "$SMPROGRAMS\Battleships Clash"
	CreateShortcut "$SMPROGRAMS\Battleships Clash\Battleships Clash.lnk" "$INSTDIR\Battleships_Clash.exe"
    CreateShortcut "$SMPROGRAMS\Battleships Clash\Uninstall Battleships Clash.lnk" "$INSTDIR\BattleshipsClash_Uninstaller.exe"
	CreateShortcut "$DESKTOP\Battleships Clash.lnk" "$INSTDIR\Battleships_Clash.exe"
SectionEnd

Section "uninstall"
	# Delete files
	RMDir /r "$INSTDIR\Battleships_Clash_Data"
	Delete "$INSTDIR\Battleships_Clash.exe"
	Delete "$INSTDIR\BattleshipsClash_Icon.ico"
	
	# Delete reg keys for uninstaller
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\BattleshipsClash"
 
    # Delete shortcuts
	RMDir /r  "$SMPROGRAMS\Battleships Clash"
	Delete "$DESKTOP\Battleships Clash.lnk"
	
	# Delete uninstaller and install dir
    Delete "$INSTDIR\BattleshipsClash_Uninstaller.exe"
    RMDir $INSTDIR
SectionEnd