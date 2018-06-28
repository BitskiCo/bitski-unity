all: build fetch

build:
	$(MAKE) -C Bitski.iOS build
	mkdir -p Build/Plugins/iOS && cp -r Bitski.iOS/Build/Products/Release/*.framework Bitski.iOS/*.meta Build/Plugins/iOS/
#	mkdir -p Build/Plugins/iOS && cp -r Bitski.iOS/Build/Products/Release/*.framework Build/Plugins/iOS/ && cp -r Bitski.iOS/BitskiUnitySDK/Bitski.*  Build/Plugins/iOS/
	cp -r Bitski.Shared/*.cs Build/

fetch:
	cd Build/Plugins && curl -L -O https://github.com/Nethereum/Nethereum/releases/download/2.2.1/Nethereum.Unity.zip && unzip Nethereum.Unity.zip && rm Nethereum.Unity.zip

clean:
	$(MAKE) -C Bitski.iOS clean
	rm -rf Build/Plugins/iOS Build/*.cs
