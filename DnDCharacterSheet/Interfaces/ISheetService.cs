﻿using Models;
using Enums;
using DTOs;

namespace Interfaces
{
    public interface ISheetService
    {
        void SetStrategy(IAttributeSettingStrategy strategy);
        Sheet SetStrengthAttribute(Sheet sheet, int value);
    }
}
