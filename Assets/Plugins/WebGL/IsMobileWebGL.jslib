var CheckMobile= {
   CheckMobile: function()
   {
      return /iPhone|iPad|iPod|Android/i.test(navigator.userAgent);
   }
};  

var ExtendedCheckMobile = {

    ExtendedCheckMobile : function()
    {
        var userAgent = navigator.userAgent;
        isMobile = (
                    /\b(BlackBerry|webOS|iPhone|IEMobile)\b/i.test(userAgent) ||
                    /\b(Android|Windows Phone|iPad|iPod)\b/i.test(userAgent) ||
                    // iPad on iOS 13 detection
                    (userAgent.includes("Mac") && "ontouchend" in document)
                );
        return isMobile;
    }
 
};

var IsIOS = {
    IsIOS: function () {
        var userAgent = navigator.userAgent;
        return (/iPhone|iPad|iPod/i.test(navigator.userAgent)) || (userAgent.includes("Mac") && "ontouchend" in document);
    }
};


mergeInto(LibraryManager.library, CheckMobile);
mergeInto(LibraryManager.library, ExtendedCheckMobile);
mergeInto(LibraryManager.library, IsIOS);