import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { asyncStatus } from '../AsyncStatus';
import { authFetch } from '../../auth/AuthFetch';

export const uploadProducts = createAsyncThunk(
    "uploadProducts/upload",
    async (_, thunkApi) => {
        if (thunkApi.getState().uploadProducts.products.length === 0) {
            throw "There are no products";
        }
        await authFetch("/api/product/submit", { 
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }, 
            signal: thunkApi.signal,
            body: JSON.stringify(thunkApi.getState().uploadProducts.products),
        })
            .catch(console.error);
    }
);

export const uploadProductsSlice = createSlice({
    name: "uploadProducts",
    initialState: {
        products: [],
        status: asyncStatus.IDLE,
        error: null
    },
    reducers: {
        addItem: (state, action) => {
            state.products.push(action.payload);
        },
        deleteItem: (state, action) => {
            state.products = state.products.filter(item => item.id !== action.payload);
        },
        deleteAll: (state) => {
            state.products = [];
        },
        updateItem: (state, action) => {
            let index = state.products.findIndex(item => item.id === action.payload.id);
            if (index > -1) {
                state.products[index] = action.payload;
            }
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(uploadProducts.fulfilled, state => {
                state.status = asyncStatus.SUCCESS;
                state.products = [];
            })
            .addCase(uploadProducts.pending, state => {
                state.status = asyncStatus.LOADING;
            })
            .addCase(uploadProducts.rejected, (state, action) => {
                state.status = asyncStatus.ERROR;
                state.error = action.error;
            });
    }
});

export const { addItem, deleteItem, deleteAll, updateItem } = uploadProductsSlice.actions;

export const uploadProductsSelector = store => store.uploadProducts.products;
export const uploadProductsStatusSelector = store => store.uploadProducts.status;
export const uploadProductsErrorSelector = store => store.uploadProducts.error;