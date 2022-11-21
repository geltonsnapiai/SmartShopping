import { createAsyncThunk, createSlice, isPending, isRejected } from "@reduxjs/toolkit";
import { authFetch } from "../../auth/AuthFetch";
import { asyncStatus } from "../AsyncStatus";

export const loadProducts = createAsyncThunk(
    'productList/load',
    async (query, thunkAPI) => {
        const url = "/api/product?" + new URLSearchParams({search: query});
        return await authFetch(url, { signal: thunkAPI.signal })
            .then(res => res.json())
            .catch(console.error);
    },
);

export const productListSlice = createSlice({
    name: "productList",
    initialState: {
        productList: [],
        status: asyncStatus.IDLE,
        error: null,
    },
    reducers: { },
    extraReducers: (builder) => {
        builder
            .addCase(loadProducts.fulfilled, (state, action) => {
                state.status = asyncStatus.SUCCESS;
                state.productList = action.payload;
            })
            .addMatcher(isPending(loadProducts), state => {
                state.status = asyncStatus.LOADING;
            })
            .addMatcher(isRejected(loadProducts), (state, action) => {
                state.status = asyncStatus.ERROR;
                state.error = action.error;
            });
    }
});

export const productListSelector = store => store.productList.productList;
export const productListStatusSelector = store => store.productList.status;
export const productListErrorSelector = store => store.productList.error;