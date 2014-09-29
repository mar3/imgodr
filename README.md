imgodr
======
[![Gitter](https://badges.gitter.im/Join Chat.svg)](https://gitter.im/mar3/imgodr?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

##概要

写真を撮影日(EXIF: 0x9003, DateTimeOriginal)で並べ替え(rename)ます。

##補足

- Windows 8.1
- VS Express 2013 for Desktop

##使い方

ディレクトリやファイルを imgodr.exe にドラッグ&ドロップします。複数のオブジェクトを同時にドロップしても OK です。

##詳細

たとえば

```
IMG145623.JPG
xxxx - 02.jpg
xxxx - 02 - コピー.jpg
```

とあったとき

```
2013年03月22日 09時55分09秒.JPG
2014年01月10日 23時06分42秒.jpg
2014年01月10日 23時06分42秒 (1).jpg
```

のように名前が変化します。

EXIF の 0x9003 を読み取れなかったファイルは無視します。

ザックリと書いただけでアップしてみましたが、徐々にきれいにしていきたいと思います。
