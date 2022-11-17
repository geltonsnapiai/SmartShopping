import { configureStore } from '@reduxjs/toolkit';
import { productListSlice } from './slices/ProductListSlice';
import { shopsSlice } from './slices/ShopsSlice';
import { uploadProductsSlice } from './slices/UploadProductsSlice';

export const store = configureStore({
    reducer: {
        uploadProducts: uploadProductsSlice.reducer,
        productList: productListSlice.reducer,
        shops: shopsSlice.reducer,
    },
});