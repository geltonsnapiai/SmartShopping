import { configureStore } from '@reduxjs/toolkit';
import { searchProductListSlice } from './slices/SearchProductListSlice';
import { uploadListSlice } from './slices/UploadListSlice';

export const store = configureStore({
    reducer: {
        uploadList: uploadListSlice.reducer,
        searchProductList: searchProductListSlice.reducer,
    },
});