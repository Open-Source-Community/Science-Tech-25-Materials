#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>

using namespace std;

const int MAXN = 100005;
const int LOG = 20;

vector<vector<int>> tree;
vector<int> euler, depth, first_occurrence;
int st[2 * MAXN][LOG]; // Sparse table
int log_table[2 * MAXN];

// Euler Tour DFS
void dfs(int node, int d, int parent) {
    first_occurrence[node] = euler.size();
    euler.push_back(node);
    depth.push_back(d);

    for (int child : tree[node]) {
        if (child != parent) {
            dfs(child, d + 1, node);
            euler.push_back(node);
            depth.push_back(d);
        }
    }
}

// Build log table and sparse table
void buildSparseTable() {
    int n = depth.size();

    // Precompute logs
    log_table[1] = 0;
    for (int i = 2; i <= n; i++)
        log_table[i] = log_table[i / 2] + 1;

    // Initialize sparse table
    for (int i = 0; i < n; i++)
        st[i][0] = i;

    // Fill rest of table
    for (int j = 1; (1 << j) <= n; j++) {
        for (int i = 0; i + (1 << j) <= n; i++) {
            int l = st[i][j - 1];
            int r = st[i + (1 << (j - 1))][j - 1];
            st[i][j] = (depth[l] < depth[r]) ? l : r;
        }
    }
}

// Query LCA of u and v
int lca(int u, int v) {
    int left = first_occurrence[u];
    int right = first_occurrence[v];
    if (left > right)
        swap(left, right);
    int j = log_table[right - left + 1];
    int l = st[left][j];
    int r = st[right - (1 << j) + 1][j];
    return (depth[l] < depth[r]) ? euler[l] : euler[r];
}

// Example usage
int main() {
    int n = 9; // Number of nodes
    tree.resize(n);
    first_occurrence.resize(n);

    // Build the tree (same as earlier)
    tree[0] = { 1, 2 };
    tree[1] = { 3, 4 };
    tree[2] = { 5, 6 };
    tree[5] = { 7, 8 };

    // Run DFS to build Euler tour and depth array
    dfs(0, 0, -1);

    // Build sparse table on depth array
    buildSparseTable();

    // Query LCA
    int u = 4, v = 8;
    cout << "LCA of " << u << " and " << v << " is " << lca(u, v) << endl;

    return 0;
}
