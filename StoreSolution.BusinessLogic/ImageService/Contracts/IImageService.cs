﻿using System.Drawing;

namespace StoreSolution.BusinessLogic.ImageService.Contracts
{
    public interface IImageService
    {
        Image ByteArrayToImage(byte[] imageBytes);

        byte[] ImageToByteArray(Image image);

        Size GetSize(Size size, int bound);
    }
}