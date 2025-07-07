# 🧭 Lowest Common Ancestor (LCA)

## 📌 What is LCA?

The **Lowest Common Ancestor (LCA)** of two nodes in a tree is the **lowest (deepest)** node that has both nodes as descendants (a node can be a descendant of itself).

---

## 🎯 Why is LCA Important?

LCA is a fundamental concept in computer science, useful in:

- **Computational biology** (e.g., phylogenetic trees)
- **File systems** (e.g., finding common directories)
- **Version control systems** (e.g., finding merge bases)
- **Network routing** (e.g., hierarchical routing trees)
- **Competitive programming and graph theory**

It allows us to efficiently solve problems like:

- Finding the **distance** between two nodes
- Solving **range queries** on trees
- **Optimizing** dynamic programming on trees

---

## 🧮 Algorithms to Find LCA

### 1. Binary Lifting

- **Preprocessing Time:** O(N log N)
- **Query Time:** O(log N)
- Uses dynamic programming to store 2^i-th ancestors.
- Ideal for multiple online queries.

### 2. Euler Tour + RMQ (Range Minimum Query)

- **Preprocessing Time:** O(N log N)
- **Query Time:** O(1)
- Converts the tree into an array via Euler Tour.
- Uses Segment Tree or Sparse Table for RMQ.
- Very fast for online queries.

### 3. Tarjan’s Offline Algorithm

- **Total Time (for Q queries):** O(N + Q)
- Uses Union-Find (Disjoint Set Union).
- Works efficiently when all queries are known in advance (offline queries).

### 4. Naive Parent Traversal

- **Query Time:** O(N)
- Traverse parent chains until common ancestor is found.
- Simple but slow — not suitable for large trees or many queries.

---

## ✅ Summary Table

| Algorithm             | Preprocessing | Query Time | Use Case                  |
|----------------------|---------------|------------|---------------------------|
| Binary Lifting        | O(N log N)    | O(log N)   | Many **online** queries   |
| Euler Tour + RMQ      | O(N log N)    | O(1)       | Many **online** queries   |
| Tarjan’s Algorithm    | O(N + Q)      | O(1)       | Many **offline** queries  |
| Naive Traversal       | None          | O(N)       | Small trees / few queries |

---

## 🛠️ Notes

- All methods assume the tree is **rooted**.
- For dynamic trees (changing structure), more complex data structures like **Link/Cut Trees** are used.


# 🧭 Lowest Common Ancestor (LCA) in C++ Using Euler Tour + RMQ (No Classes or Structs)

This guide explains how to compute the **Lowest Common Ancestor (LCA)** in a tree using an **Euler Tour** and **Range Minimum Query (RMQ)** with a **Sparse Table**, all in **plain C++ without using classes or structs**. The tree is represented as an adjacency list using `vector<vector<int>>`.

---

## 📌 What is the LCA?

The **Lowest Common Ancestor (LCA)** of two nodes `u` and `v` in a tree is the **deepest node** that is an ancestor of both `u` and `v`. It's a fundamental operation in many tree-based algorithms and is commonly used in competitive programming and computer science research.

---

## 💡 Overview of the Approach

We solve the LCA problem using:

1. **Euler Tour**: Traverse the tree and record:
   - Each node visit.
   - Depth of each node when visited.
   - First occurrence of each node in the tour.
2. **Range Minimum Query (RMQ)**: Use a Sparse Table to quickly query the node with minimum depth between any two positions in the Euler tour — this corresponds to the LCA.

This approach transforms the LCA problem into an RMQ problem.

---

## 🧱 Data Structures Used

- `tree`: adjacency list for representing the tree.
- `euler`: stores the order of visited nodes in the Euler tour.
- `depth`: depth of each node at time of visit in the Euler tour.
- `first_occurrence`: index of the first occurrence of each node in `euler`.
- `st[][]`: sparse table to perform range minimum queries on `depth`.
- `log_table[]`: precomputed floor of log base 2 values for fast queries.

---

## 🔣 Full C++ Implementation

```cpp
#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
using namespace std;

const int MAXN = 100005; // max number of nodes
const int LOG = 20;      // log2(MAXN)

vector<vector<int>> tree;
vector<int> euler, depth, first_occurrence;
int st[2 * MAXN][LOG];
int log_table[2 * MAXN];

// Perform DFS to generate Euler tour
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

// Build sparse table over the depth array
void buildSparseTable() {
    int n = depth.size();

    log_table[1] = 0;
    for (int i = 2; i <= n; i++)
        log_table[i] = log_table[i / 2] + 1;

    for (int i = 0; i < n; i++)
        st[i][0] = i;

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
    if (left > right) swap(left, right);
    int j = log_table[right - left + 1];
    int l = st[left][j];
    int r = st[right - (1 << j) + 1][j];
    return (depth[l] < depth[r]) ? euler[l] : euler[r];
}

// Example usage
int main() {
    int n = 9; // number of nodes
    tree.resize(n);
    first_occurrence.resize(n);

    // Build tree
    tree[0] = {1, 2};
    tree[1] = {3, 4};
    tree[2] = {5, 6};
    tree[5] = {7, 8};

    // Step 1: Perform Euler tour starting from root
    dfs(0, 0, -1);

    // Step 2: Build RMQ over depth array
    buildSparseTable();

    // Step 3: Answer LCA queries
    int u = 4, v = 8;
    cout << "LCA of " << u << " and " << v << " is: " << lca(u, v) << endl;

    return 0;
}


        0
       / \
      1   2
     / \   \
    3   4   5
           / \
          7   8



- Node `0` is the root.
- Edges:  
  - `0 → 1, 2`  
  - `1 → 3, 4`  
  - `2 → 5`  
  - `5 → 7, 8`

---

## 📈 Euler Tour and Depth

The **Euler Tour** is the sequence of nodes visited during a depth-first traversal where we record a node both when we enter and after finishing each child.

- **Euler Tour (Nodes):**  
  `[0, 1, 3, 1, 4, 1, 0, 2, 5, 7, 5, 8, 5, 2, 0]`

- **Depth at Each Step:**  
  `[0, 1, 2, 1, 2, 1, 0, 1, 2, 3, 2, 3, 2, 1, 0]`

- **First Occurrence in Euler Tour:**  
  - `first_occurrence[3] = 2`  
  - `first_occurrence[4] = 4`  
  - `first_occurrence[5] = 8`  
  - `first_occurrence[7] = 9`  
  - `first_occurrence[8] = 11`  

---

## ❓ Sample Query: `LCA(4, 8)`

### Step 1: Find First Occurrences
- `first_occurrence[4] = 4`
- `first_occurrence[8] = 11`

### Step 2: Query RMQ on `depth[4..11]`
We extract the depths from index 4 to 11:

- Subarray of depths: `[2, 1, 0, 1, 2, 3, 2, 3]`

Minimum value = `0`  
→ This occurs at index `6` in the full Euler tour  
→ `euler[6] = 0`

### ✅ Result:
`LCA(4, 8) = 0`

---

## 🧠 Intuition

- The LCA of two nodes is the **lowest (deepest) node** that is an ancestor of both.
- By using the **Euler tour**, we flatten the tree into an array where LCA queries become RMQ problems on the **depth** array.
- The node with the **minimum depth** between the **first appearances** of the two nodes in the Euler tour is their LCA.

---

## 🏁 Conclusion

- **LCA(4, 8) = 0**
- Works in **O(1)** query time after **O(N log N)** preprocessing.
- Scales well to large trees and multiple queries.
