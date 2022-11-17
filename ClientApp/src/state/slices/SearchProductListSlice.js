import { createAsyncThunk, createSlice, isPending, isRejected } from "@reduxjs/toolkit";
import { authFetch } from "../../auth/AuthFetch";
import { asyncStatus } from "../AsyncStatus";

export const loadProducts = createAsyncThunk(
    'searchProductList',
    async (query, thunkAPI) => {
        const url = "/api/product?" + new URLSearchParams({search: query});
        return await authFetch(url, { signal: thunkAPI.signal })
            .then(res => res.json())
            .catch(console.error);
    },
);

export const searchProductListSlice = createSlice({
    name: "searchProductList",
    initialState: {
        productList: [],
        asyncStatus: asyncStatus.IDLE,
        error: null,
    },
    reducers: {
        deleteAll: (state) => {
            state.productList = [];
            state.asyncStatus = asyncStatus.IDLE;
            state.error = null;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(loadProducts.fulfilled, (state, action) => {
                state.asyncStatus = asyncStatus.SUCCESS;
                state.productList = action.payload;
            })
            .addMatcher(isPending(loadProducts), state => {
                state.asyncStatus = asyncStatus.LOADING;
            })
            .addMatcher(isRejected(loadProducts), (state, action) => {
                state.asyncStatus = asyncStatus.ERROR;
                state.error = action.error;
            });
    }
});

export const { deleteAll } = searchProductListSlice.actions;

export const searchProductListSelector = store => store.searchProductList.productList;
export const searchProductListAsyncStatusSelector = store => store.searchProductList.asyncStatus;
export const searchProductListErrorSelector = store => store.searchProductList.error;