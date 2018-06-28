//
//  BitskiUnitySDK.h
//  BitskiUnitySDK
//
//  Created by Patrick Tescher on 6/25/18.
//  Copyright © 2018 Out There Labs. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <SafariServices/SafariServices.h>
#import <AppAuth/AppAuth.h>

#ifndef BitskiUnitySDK
#define BitskiUnitySDK
extern "C"
{
    typedef void(*BitskiSignInCallback)(const char *token, const char *error);
    void BitskiSignIn(char *clientID, char *redirectUri, BitskiSignInCallback callback);
}
#endif
